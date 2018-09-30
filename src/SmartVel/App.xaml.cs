using System.Globalization;
using Prism;
using Prism.Ioc;
using Prism.Modularity;
using SmartVel.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using SmartVel.Modules;
using SmartVel.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SmartVel
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null)
        {
        }

        public App(IPlatformInitializer initializer) : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            Application.Current.Migrate();
            await NavigationService.NavigateAsync("Navigation/Login");
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ViewConfig.Init(containerRegistry);
            ServiceConfig.Init(containerRegistry);
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            ModuleConfig.Init(moduleCatalog);
        }
    }
}
