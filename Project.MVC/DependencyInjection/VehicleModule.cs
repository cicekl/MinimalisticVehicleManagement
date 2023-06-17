using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using Ninject.Web.Common;
using Project.MVC.Mapping;
using Project.Service.DataAccess;
using Project.Service.Services;

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
          //  Bind<IMapper>().To<Mapper>().InSingletonScope();
            Bind<IVehicleService>().To<VehicleService>().InRequestScope();
        }
    }
}
