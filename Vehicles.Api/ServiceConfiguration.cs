using Vehicles.Api.Repositories;
using Vehicles.Api.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            //Have to add vehicle repo this way as .NET DI has odd behaviour with IEnumerable parameters.
            services.AddScoped<IVehiclesRepository>(c => new VehiclesRepository());
            services.AddScoped<IVehiclesService, VehiclesService>();
            return services;
        }
    }
}
