using SQLite;

namespace SmartVel.Models
{
    /// <summary>
    /// Entité distionnaire
    /// </summary>
    [Table("reference")]
    public class Reference : Entity
    {
        /// <summary>
        /// code de la reference
        /// </summary>
        [Column(name:"code")]
        public string Code { get; set; }
        /// <summary>
        /// domaine fonctionnel de la référence
        /// </summary>
        [Column(name: "domain")]
        public string Domain { get; set; }
        /// <summary>
        /// valeur de la référence
        /// </summary>
        [Column(name: "value")]
        public string Value { get; set; }
        /// <summary>
        /// description de la référence
        /// </summary>
        [Column(name: "description")]
        public string Description { get; set; }
    }
}
