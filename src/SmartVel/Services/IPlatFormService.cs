namespace SmartVel.Services
{
    /// <summary>
    /// interface de service de gestion de plateform
    /// </summary>
    public interface IPlatFormService
    {
        /// <summary>
        /// chemin absolu du fichier
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string GetLocalFilePath(string fileName);

        /// <summary>
        /// plateform informations
        /// </summary>
        /// <returns></returns>
        string GetPlatformInfo();

        /// <summary>
        /// version plateforme
        /// </summary>
        /// <returns></returns>
        string GetPlaformInfoVersion();
    }
}
