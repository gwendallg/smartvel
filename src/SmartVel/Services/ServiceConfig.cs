using Prism.Ioc;
using Prism.Unity;
using Unity;

namespace SmartVel.Services
{
    public static class ServiceConfig
    {
        public static void Init(IContainerRegistry containerRegistry)
        {
            // enregistrement des service
            containerRegistry.RegisterSingleton<IMigrationService, MigrationService>();
            containerRegistry.Register(typeof(IDbGenericService<>), typeof(DbGenericService<>));
            containerRegistry.Register<IUserService, UserService>();

            // on applique les migrations sur la bae de données
            var migrationService =
                containerRegistry.GetContainer().Resolve(typeof(IMigrationService)) as IMigrationService;
            migrationService?.Migrate();
/*
#if DEBUG
            containerRegistry.RegisterSingleton<IMockService, MockService>();
            var mockService =
                containerRegistry.GetContainer().Resolve(typeof(IMockService)) as IMockService;
            mockService?.Cows();
#endif
*/
        }
    }
}
