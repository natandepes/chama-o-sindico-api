using ChamaOSindico.Infra.Context;
using ChamaOSindico.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChamaOSindico.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));

            // register repositories
            services.AddScoped<ProductRepository>();

            // register services

            return services;
        }
    }
}
