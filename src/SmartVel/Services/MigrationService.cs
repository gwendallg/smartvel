using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using SmartVel.Exceptions;
using SmartVel.Models;
using SQLite;
using Xamarin.Forms;

namespace SmartVel.Services
{
    public class MigrationService : DbGenericService<Migration>,  IMigrationService
    {
        private readonly Regex _regex = new Regex(@"V([0-9]+)__(.*)\.sql");
        private readonly ICryptographyService _cryptographyService;

        /// <summary>
        /// initialise une nouvelle instance de la classe
        /// </summary>
        public MigrationService()
        {
            _cryptographyService = DependencyService.Resolve<ICryptographyService>();
        }

        /// <summary>
        /// applique la migration 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="migration"></param>
        /// <param name="script"></param>
        private List<Migration> ApplyMigration(SQLiteConnection connection, Migration migration)
        {
            connection.BeginTransaction();
            var queries = migration.Script.Split(';');
            foreach (var query in queries)
            {
                if (string.IsNullOrWhiteSpace(query)) continue;
                connection.Execute(query);
            }
            Insert(migration, connection);
            connection.Commit();
            return connection.Table<Migration>().ToList();
        }

        /// <summary>
        /// essaye de construire une nouvelle migration si celle-ci n'a pas déjà été appliquée à la base de données
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="migrations"></param>
        /// <param name="script"></param>
        /// <returns></returns>
        private Migration TryBuildNewMigration(string resource, IEnumerable<Migration> migrations)
        {
            var match = _regex.Match(resource);
            // extraction de l'indentifiant de la migration   
            var id = Convert.ToInt32(match.Groups[1].Value, CultureInfo.InvariantCulture);
            var name = match.Groups[2].Value;
            string script;
            // recherche de la migration si elle a déjà été jouée
            var exist = migrations.SingleOrDefault(m =>
                m.Id == id);
            string checksum;
            // désérialisation de resource
            using (var s = GetType().Assembly.GetManifestResourceStream(resource))
            {
                using (var sr = new StreamReader(s ?? throw new InvalidOperationException()))
                {
                    script = sr.ReadToEnd();
                    checksum = _cryptographyService.Md5(script);
                }
            }

            if (exist != null)
            {
                // migration exist et est valide
                if (exist.Checksum == checksum)
                {
                    Debug.WriteLine($"migration checked : {exist}");
                    return null;
                }

                throw new ByResourceApplicationException($"invalid checksum for migration {exist}");
            }


            // création de la migration
            return new Migration()
            {
                Id = id,
                Name = name,
                Script = script,
                Checksum = checksum,
                CreatedAt = "system",
                LastModifiedAt = "system",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            };
        }

        /// <inheritdoc />
        public void Migrate()
        {
            using (var connection = CreateConnection())
            {
                // création de la table des migration si elle n'exite pas
                connection.CreateTable<Migration>();
                // récupération des migrations déjà appliquées en base de données
                var migrations = connection.Table<Migration>().ToList();
                // récupération des migrations appliquables
                var resources = GetType().Assembly.GetManifestResourceNames()
                    .Where(r => r.StartsWith(typeof(App).Namespace + ".Migrations.Db",
                        StringComparison.InvariantCulture))
                    .OrderBy(r => r);
                foreach (var resource in resources)
                {
                    var migration = TryBuildNewMigration(resource, migrations);
                    if (migration == null) continue;
                    migrations = ApplyMigration(connection, migration);
                    Debug.WriteLine($"migration applied : {migration}");
                }
            }
        }
    }
}
