using System;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism.Ioc;
using Prism.Unity;
using SmartVel.Models;
using SmartVel.Resources;
using SmartVel.Services;
using SmartVel.Utils;

// ReSharper disable once CheckNamespace
namespace Xamarin.Forms
{
    public static class ApplicationExtensions
    {
        #region User 
        /// <summary>
        /// retourne l'utilisateur courant de l'application
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public static User GetCurrentUser(this Application application)
        {
            if (application == null) throw new ArgumentNullException(nameof(application));
            lock (application)
            {
                if (application.Properties.ContainsKey("user")) return application.Properties?["user"] as User;
                return null;
            }
        }

        /// <summary>
        /// met à jour l'utilisateur courant de l'application
        /// </summary>
        /// <param name="application"></param>
        /// <param name="user"></param>
        public static void SetCurrentUser(this Application application, User user)
        {
            if (application == null) throw new ArgumentNullException(nameof(application));
            lock (application)
            {
                application.Properties[nameof(user)] = user;
            }
        }

        #endregion

        #region Culture

        /// <summary>
        /// retourne la culture courante de l'application
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public static CultureInfo GetCurrentCulture(this Application application)
        {
            if (application == null) throw new ArgumentNullException(nameof(application));
            lock (application)
            {
                if (application.Properties.ContainsKey("culture"))
                    return application.Properties?["culture"] as CultureInfo;
                return CultureInfo.GetCultureInfo("fr");
            }
        }

        /// <summary>
        /// met à jour la culture courante de l'application
        /// </summary>
        /// <param name="application"></param>
        /// <param name="culture"></param>
        public static void SetCurrentCulture(this Application application, CultureInfo culture)
        {
            if (application == null) throw new ArgumentNullException(nameof(application));
            lock (application)
            {
                application.Properties[nameof(culture)] = culture;
            }
        }

        #endregion

        #region Translate

        private static readonly ResourceManager ResourceManager = AppResources.ResourceManager;
        public static string Translate(this Application application, string id,
            params object[] args)
        {
            return Translate(application, id, StartCaseType.UpperCase, GetCurrentCulture(application), args);
        }

        public static string Translate(this Application application, string id, StartCaseType startCase,
            params object[] args)
        {
            return Translate(application, id, startCase, CultureInfo.CurrentUICulture, args);
        }

        public static string Translate(this Application application, string id, CultureInfo culture,
            params object[] args)
        {
            return Translate(application, id, StartCaseType.UpperCase, culture, args);
        }

        public static string Translate(this Application application,
            string id,
            StartCaseType startCase, CultureInfo cultureInfo, params object[] args)
        {
            if (application == null) throw new ArgumentNullException(nameof(application));
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
            if (cultureInfo == null) throw new ArgumentNullException(nameof(cultureInfo));
            var result = ResourceManager.GetString(id, cultureInfo) ?? id;
            if (startCase == StartCaseType.LowerCase)
            {
                var resultStart = char.ToLowerInvariant(result[0]);
                if (result.Length == 1) return resultStart.ToString();
                result = resultStart + result.Substring(1);
            }

            if (args != null && args.Any()) result = string.Format(result, args);
            return result;
        }

        #endregion

        #region Waiting

        public static void Waiting(this Application application)
        {
            if (application == null) throw new ArgumentNullException(nameof(application));
            Device.BeginInvokeOnMainThread(() =>
            {
                UserDialogs.Instance.ShowLoading(Translate(application, "Attente"), MaskType.Black);
            });
        }

        public static void Waiting(this Application application, string text)
        {
            if (application == null) throw new ArgumentNullException(nameof(application));
            if (text == null) throw new ArgumentNullException(nameof(text));
            Device.BeginInvokeOnMainThread(() =>
            {
                UserDialogs.Instance.ShowLoading(text, MaskType.Black);
            });
        }

        #endregion

        #region Dismiss

        public static void Dismiss(this Application application)
        {
            if (application == null) throw new ArgumentNullException(nameof(application));
            Device.BeginInvokeOnMainThread(() =>
            {
                UserDialogs.Instance.HideLoading();
            });
        }

        #endregion

        #region ShowToast

        public static void ShowToast(this Application application, string toast, Color foreColor,
            Color backColor)
        {
            if (application == null) throw new ArgumentNullException(nameof(application));
            if (toast == null) throw new ArgumentNullException(nameof(toast));
            Device.BeginInvokeOnMainThread(() =>
            {
                var conf = new ToastConfig(toast);
                conf.SetDuration(1500);
                conf.SetPosition(ToastPosition.Top);
                conf.SetMessageTextColor(foreColor);
                conf.SetBackgroundColor(backColor);
                UserDialogs.Instance.Toast(conf);
            });
        }

        #endregion

        #region AskForPermission

        public static async Task<bool> AskForPermission(this Application application, Permission permission)
        {
            return await AskForPermission(application, permission, application.GetCurrentCulture());
        }

        public static async Task<bool> AskForPermission(this Application application, Permission permission,
            CultureInfo cultureInfo)
        {
            try
            {
                if (application == null) throw new ArgumentNullException(nameof(application));
                if (cultureInfo == null) throw new ArgumentNullException(nameof(cultureInfo));
                var translatePermission = Translate(application, permission.ToString(), cultureInfo);
                var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
                if (permissionStatus == PermissionStatus.Denied)
                {
                    var title = Translate(application, "PermissionTitre", cultureInfo, translatePermission);
                    var question = Translate(application, "PermissionQuestion", cultureInfo, translatePermission);
                    var positive = Translate(application, "PermissionOuvrirParametres", cultureInfo);
                    var negative = Translate(application, "PermissionPlusTard", cultureInfo);
                    var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                    if (task == null) return false;
                    if (await task)
                    {
                        CrossPermissions.Current.OpenAppSettings();
                    }

                    return false;
                }

                if (permissionStatus != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(permission))
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        await Application.Current?.MainPage?.DisplayAlert(
                            Translate(application, "PermissionTitre", cultureInfo),
                            Translate(application, $"PermissionJustification{permission}", cultureInfo),
                            Translate(application, "Ok", cultureInfo));
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(permission))
                        permissionStatus = results[permission];
                }

                if (permissionStatus == PermissionStatus.Granted)
                {
                    return true;
                }

                // ReSharper disable once PossibleNullReferenceException
                await Application.Current?.MainPage?.DisplayAlert(
                    Translate(application, "PermissionTitre", cultureInfo),
                    Translate(application, $"PermissionRefus{permission}", cultureInfo),
                    Translate(application, "Ok", cultureInfo));
            }
            catch (Exception ex)
            {
                Insight.TrackError(ex);

            }

            return false;
        }

        #endregion

        public static void Migrate(this Application application)
        {
            if (application == null) throw new ArgumentNullException(nameof(application));
            var migrationService = ((PrismApplication) application).Container.Resolve<IMigrationService>();
            migrationService.Migrate();
        }
    }
}
