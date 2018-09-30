using System;
using System.IO;
using SmartVel.Droid.Services;
using SmartVel.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(PlatFormService))]
namespace SmartVel.Droid.Services
{
    /// <summary>
    /// implémentation du service plateforme pour Android
    /// </summary>
    public class PlatFormService : IPlatFormService
    {
        /// <inheritdoc />
        public string GetLocalFilePath(string fileName)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName);
        }

        /// <inheritdoc />
        public string GetPlatformInfo()
        {
            return
                $"Android - Manufacturer: ${Android.OS.Build.Manufacturer}, Product: ${Android.OS.Build.Product}, Model: ${Android.OS.Build.Model}";
        }

        /// <inheritdoc />
        public string GetPlaformInfoVersion()
        {
            return Android.OS.Build.VERSION.Sdk;
        }
    }
}