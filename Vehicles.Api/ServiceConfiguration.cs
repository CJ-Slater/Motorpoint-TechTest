using Vehicles.Api.Repositories;
using Vehicles.Api.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IVehiclesRepository>(new VehiclesRepository());
            services.AddSingleton<VehiclesRepository, VehiclesRepository>();
            services.AddSingleton<VehiclesService, VehiclesService>();
            return services;
        }
    }
}
