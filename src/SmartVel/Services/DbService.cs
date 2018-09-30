using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace SmartVel.Services
{
    /// <summary>
    /// implémenation du service d'accès à la base de données
    /// </summary>
    public abstract class DbService : IDbService
    {
        private static readonly Lazy<string> DatabasePathLazy = new Lazy<string>(() =>
            DependencyService.Get<IPlatFormService>().GetLocalFilePath("database.db"));

        /// <inheritdoc />
        public SQLiteConnection CreateConnection()
        {
            return new SQLiteConnection(DatabasePathLazy.Value);
        }

        public async Task<SQLiteConnection> CreateConnectionAsync()
        {
            return await Task.Run(() => CreateConnection());
        }

        
        #region Query

        public IList<T> Query<T>(string query, SQLiteConnection connection = null, params object[] args) where T : new()
        {
            if (connection != null)
                return OnQuery<T>(query, connection);
            using (var innerConnection = CreateConnection())
            {
                return OnQuery<T>(query, innerConnection);
            }
        }

        private static IList<T> OnQuery<T>(string query, SQLiteConnection connection,
            params object[] args) where T: new()
        {
            if (string.IsNullOrWhiteSpace(query)) throw new ArgumentNullException(nameof(query));
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            return connection.Query<T>(query, args);
        }

        #endregion

        #region QueryAsync

        public async Task<IList<T>> QueryAsync<T>(string query, SQLiteConnection connection = null, params object[] args) where T: new()
        {
            if (connection != null)
                return await OnQueryAsync<T>(query, connection);
            using (var innerConnection = CreateConnection())
            {
                return await OnQueryAsync<T>(query, innerConnection);
            }
        }

        private static Task<IList<T>> OnQueryAsync<T>(string query, SQLiteConnection connection,
            params object[] args) where T : new()
        {
            if (string.IsNullOrWhiteSpace(query)) throw new ArgumentNullException(nameof(query));
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            return Task<IList<T>>.Factory.StartNew(() => OnQuery<T>(query, connection, args));
        }

        #endregion
    }
}
