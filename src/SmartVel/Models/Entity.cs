using System;
using SQLite;

namespace SmartVel.Models
{
    /// <summary>
    /// classe abstraite déclarant une entité
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// identifiant unique de la migration
        /// </summary>
        [PrimaryKey, Column("id"), AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// date de création
        /// </summary>
        [Column("created_date"), NotNull]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// date de dernière mise à jour de l'enregistrement
        /// </summary>
        [Column("last_modified_date"), NotNull]
        public DateTime? LastModifiedDate { get; set; }

        /// <summary>
        /// utilisateur qui à créé l'enregistrement
        /// </summary>
        [Column("created_at"), MaxLength(255)]
        public string CreatedAt { get; set; }

        /// <summary>
        ///  dernier utilisateur qui à mise à jour l'enregistrement
        /// </summary>
        [Column("last_modified_at"), MaxLength(255)]
        public string LastModifiedAt { get; set; }
    }
}
