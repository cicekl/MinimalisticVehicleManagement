using Ninject.Modules;
using Ninject.Web.Common;
using Project.Services.DataAccess;
using Project.Services.Services;

namespace Project.MVC.DependencyInjection
{
    public class VehicleModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IConfiguration>().ToMethod(ctx =>
                 new ConfigurationBuilder()
                     .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json")
                     .Build())
                 .InSingletonScope();
            Bind<VehicleDBContext>().ToSelf().InRequestScope();
            Bind<IMakeService>().To<MakeService>().InRequestScope();
            Bind<IModelService>().To<ModelService>().InRequestScope();

        }
    }
}
