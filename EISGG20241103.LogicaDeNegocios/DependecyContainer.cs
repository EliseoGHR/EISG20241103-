using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EISGG20241103.AccesoADatos;

namespace EISGG20241103.LogicaDeNegocios
{
    public static class DependecyContainer
    {
        public static IServiceCollection AddBLDependecies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDALDependecies(configuration);
            services.AddScoped<ClienteBL>();
            return services;
        }
    }
}
