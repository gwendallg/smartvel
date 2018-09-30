namespace SmartVel.Services
{
    /// <summary>
    /// interface de gestion des migration de base de données
    /// </summary>
    public interface IMigrationService : IDbService
    {
        /// <summary>
        /// migre la base de données
        /// </summary>
        void Migrate();
    }
}
