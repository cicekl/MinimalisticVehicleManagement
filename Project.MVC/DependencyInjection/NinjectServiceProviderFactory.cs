using Ninject;


namespace Project.MVC.DependencyInjection
{
    public class NinjectServiceProviderFactory : IServiceProviderFactory<IServiceCollection>
    {

        private readonly IKernel _kernel;

        public NinjectServiceProviderFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IServiceCollection CreateBuilder(IServiceCollection services)
        {
            return services;
        }

        public IServiceProvider CreateServiceProvider(IServiceCollection services)
        {
            _kernel.Load(new VehicleModule());
            return _kernel;
        }
    }
}
