using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Prism.Commands;
using Prism.Navigation;
using SmartVel.Services;
using Xamarin.Forms;

namespace SmartVel.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {

        private readonly IUserService _userService;

        private string _login;

        private string _password;

        private bool _isRememberMe;

        private CultureInfo _culture;

        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public bool IsRememberMe
        {
            get => _isRememberMe;
            set => SetProperty(ref _isRememberMe, value);
        }

        public CultureInfo Culture
        {
            get => _culture;
            set => SetProperty(ref _culture, value);
        }

        public ICommand ConnectCommand { get; }

        public LoginPageViewModel(INavigationService navigationService, IUserService userService) : base(
            navigationService)
        {
            Culture = Application.Current.GetCurrentCulture();
            ConnectCommand = new DelegateCommand(async () => await OnConnectAsync());
            _userService = userService;
        }

        private async Task OnConnectAsync()
        {
            try
            {
                Application.Current.Waiting();
                await _userService.ConnectAsync(Login, Password, IsRememberMe);
                Application.Current.Dismiss();
            }
            catch (Exception e)
            {
                Application.Current.Dismiss();
                UserDialogs.Instance.Alert(new AlertConfig()
                {
                    Message = e.Message,
                    OnAction = () =>
                    {
                        Login = string.Empty;
                        Password = string.Empty;
                    }
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey(Constants.ReferenceCodes.DefaultLanguage))
            {
                //Translate();
            }

            if (parameters.ContainsKey(Constants.NavigationSemaphore.LogOut))
            {
                Password = "";
            }
        }
    }
}
