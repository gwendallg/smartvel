using System;
using System.IO;
using Foundation;
using SmartVel.iOS.Services;
using SmartVel.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PlatFormService))]
namespace SmartVel.iOS.Services
{
    /// <summary>
    /// implémentation du service plateforme pour iOS
    /// </summary>
    public class PlatFormService : IPlatFormService
    {
        /// <inheritdoc />
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");
            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }

        /// <inheritdoc />
        public string GetPlatformInfo()
        {
            return "iOS";
        }

        /// <inheritdoc />
        public string GetPlaformInfoVersion()
        {
            return UIDevice.CurrentDevice.SystemVersion;
        }
    }
}