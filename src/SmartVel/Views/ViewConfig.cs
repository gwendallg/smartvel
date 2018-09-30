using Prism.Ioc;
using Xamarin.Forms;

namespace SmartVel.Views
{
    public static class ViewConfig 
    {
        public static void Init(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>("Navigation");
            containerRegistry.RegisterForNavigation<MainPage>("Main");
            containerRegistry.RegisterForNavigation<LoginPage>("Login");
        }
    }
}
