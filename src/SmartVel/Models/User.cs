using SQLite;

namespace SmartVel.Models
{
    /// <summary>
    /// Entité utilisateur
    /// </summary>
    [Table("user")]
    public class User : Entity
    { 
        /// <summary>
        /// identifiant de l'utilisateur
        /// </summary>
        [Column(name: "login"), NotNull] 
        public string Login { get; set; }
        /// <summary>
        /// salt
        /// </summary>
        [Column(name: "salt"), NotNull] 
        public string Salt { get; set; }
        /// <summary>
        /// hash
        /// </summary>
        [Column(name: "hash"), NotNull] 
        public string Hash { get; set; }
    }
}
