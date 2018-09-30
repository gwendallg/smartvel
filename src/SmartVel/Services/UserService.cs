using System.Linq;
using System.Threading.Tasks;
using Prism.Navigation;
using SmartVel.Exceptions;
using SmartVel.Models;
using Xamarin.Forms;

namespace SmartVel.Services
{
    public class UserService : DbGenericService<User>, IUserService
    {

        private readonly ICryptographyService _cryptographyService;
        private readonly INavigationService _navigationService;

        /// <summary>
        /// initialise une nouvelle instance de la classe
        /// </summary>
        public UserService(INavigationService navigationService)
        {
            _cryptographyService = DependencyService.Resolve<ICryptographyService>();
            _navigationService = navigationService;
        }

        /// <inheritdoc />
        public async Task ConnectAsync(string login, string password, bool remember)
        {
            using (var connection = CreateConnection())
            {
                var usernameToInvariant = login.ToLowerInvariant();
                var user = (Where(u => u.Login == usernameToInvariant, connection)).SingleOrDefault();
                if (user == null) throw new ByResourceApplicationException("IdentifiantOuMotDePasseInvaalide");
                var hash = _cryptographyService.Sha256(string.Concat(password, user.Salt));
                if (hash != user.Hash) throw new ByResourceApplicationException("IdentifiantOuMotDePasseInvaalide");
                if (remember)
                {
                    var reference = connection.Table<Reference>().Single(r =>
                        r.Domain == Constants.ReferenceDomains.Parameters &&
                        r.Code == Constants.ReferenceCodes.RememberLogin);
                    reference.Value = login;
                    connection.Update(reference);
                }
                Application.Current.SetCurrentUser(user);
                await _navigationService.NavigateAsync("Main");
            }
        }

        /// <inheritdoc />
        public async Task DisconnectAsync()
        {
            Application.Current.SetCurrentUser(null);
            var parameters = new NavigationParameters {{Constants.NavigationSemaphore.LogOut, true}};
            await _navigationService.GoBackToRootAsync(parameters);
        }
    }
}
