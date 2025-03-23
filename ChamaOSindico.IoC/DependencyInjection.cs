using ChamaOSindico.Infra.ConfigurationFiles;
using ChamaOSindico.Infra.Context;
using ChamaOSindico.Infra.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoFramework;

namespace ChamaOSindico.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Load MongoDB Settings
            var connectionString = configuration.GetValue<string>("MongoDbSettings:ConnectionString");
            var databaseName = configuration.GetValue<string>("MongoDbSettings:DatabaseName");

            // Register MongoDB Context
            services.AddSingleton<MongoAppDbContext>(sp => new MongoAppDbContext(connectionString, databaseName));

            // Register MongoDB Connection
            services.AddSingleton<IMongoDbConnection>(sp =>
            {
                var settings = sp.GetRequiredService<MongoDbSettings>();
                return MongoDbConnection.FromConnectionString($"{settings.ConnectionString}/{settings.DatabaseName}");
            });

            // Register Repositories
            services.AddScoped<TesteIntegracaoRepository>();

            // Register Services

            return services;
        }
    }
}
