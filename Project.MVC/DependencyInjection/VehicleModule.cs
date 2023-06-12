using Ninject.Modules;
using Ninject.Web.Common;
using Project.Service.DataAccess;
using Project.Service.Services;

namespace Project.MVC.DependencyInjection
{
    public class VehicleModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IVehicleService>().To<VehicleService>().InRequestScope();
            Bind<VehicleDBContext>().ToSelf().InRequestScope();

        }
    }
}
