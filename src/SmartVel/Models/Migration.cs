using SQLite;

namespace SmartVel.Models
{
    /// <summary>
    /// Entité de gestion des migration de base de données
    /// </summary>
    [Table("schema_version")]
    public class Migration : Entity
    {
        /// <summary>
        /// valeur  du script
        /// </summary>
        [Ignore]
        public string Script { get; set; }

        /// <summary>
        /// nom de la migration
        /// </summary>
        [Column("name"), NotNull]
        public string Name { get; set; }

        /// <summary>
        /// checksum du contenu du script sql
        /// </summary>
        [Column("checksum"), NotNull]
        public string Checksum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Concat("V", Id.ToString(), "__", Name);
        }
    }
}
