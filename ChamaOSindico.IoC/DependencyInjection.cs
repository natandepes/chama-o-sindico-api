using ChamaOSindico.Application.Auth;
using ChamaOSindico.Application.Interfaces;
using ChamaOSindico.Application.Services;
using ChamaOSindico.Domain.Interfaces;
using ChamaOSindico.Application.Auth;
using ChamaOSindico.Application.Interfaces;
using ChamaOSindico.Application.Services;
using ChamaOSindico.Domain.Interfaces;
using ChamaOSindico.Infra.ConfigurationFiles;
using ChamaOSindico.Infra.Context;
using ChamaOSindico.Infra.Interfaces;
using ChamaOSindico.Infra.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MongoFramework;
using System.Text;

namespace ChamaOSindico.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Load MongoDB Settings
            var connectionString = configuration.GetValue<string>("MongoDbSettings:ConnectionString");
            var databaseName = configuration.GetValue<string>("MongoDbSettings:DatabaseName");

            services.AddSingleton<IMongoClient>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var connectionString = config.GetValue<string>("MongoDbSettings:ConnectionString");
                return new MongoClient(connectionString);
            });

            // Register MongoDB Context
            services.AddSingleton<MongoAppDbContext>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var client = sp.GetRequiredService<IMongoClient>();
                var databaseName = config.GetValue<string>("MongoDbSettings:DatabaseName");

                return new MongoAppDbContext(client, databaseName);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularClient",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();
                    });

            });

            // Register Repositories
            services.AddScoped<TesteIntegracaoRepository>();
            services.AddScoped<IComplaintRepository, ComplaintRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenBlackListRepository, MongoTokenBlacklistRepository>();
            services.AddScoped<ITransactionService, MongoTransactionService>();
            services.AddScoped<IResidentRepository, ResidentRepository>();
            services.AddScoped<IAreaRepository, AreaRepository>();
            services.AddScoped<IAreaReservationRepository, AreaReservationRepository>();
            services.AddScoped<ICondominalManagerRepository, CondominalManagerRepository>();

            // Register Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<JwtService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAreaService, AreaService>();
            services.AddScoped<IVehicleService, VehicleService>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
                AppDomain.CurrentDomain.Load("ChamaOSindico.Application"))
            );

            // Register Auth + JWT middleware
            services.AddJwtAuthentication(configuration);

            return services;
        }

        private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });

            return services;
        }
    }
}