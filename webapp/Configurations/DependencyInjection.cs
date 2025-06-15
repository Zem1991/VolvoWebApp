using VolvoWebApp.Repositories;
using VolvoWebApp.Services;

namespace VolvoWebApp.Configurations
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IVehiclesService, VehiclesService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IVehiclesRepository, VehiclesRepository>();
        }
    }
}
