using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EISGG20241103.AccesoADatos
{
    public static class DependecyContainer
    {
        public static IServiceCollection AddDALDependecies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EISG20241103DBContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("conn")));
            services.AddScoped<ClienteDAL>();

            return services;
        }
    }
}
