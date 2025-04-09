using ChamaOSindico.Application.Auth;
using ChamaOSindico.Application.Interfaces;
using ChamaOSindico.Application.Service;
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
using MongoFramework;
using System.Text;

namespace CleanArchMvc.Infra.IoC
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
            services.AddScoped<ComplaintRepository>();
            services.AddScoped<VehicleRepository>();

            services.AddScoped<UserRepository>();
            services.AddScoped<ITokenBlackListRepository, MongoTokenBlacklistRepository>();

            // Register Services
            services.AddScoped<AuthService>();
            services.AddScoped<JwtService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IComplaintService, ComplaintService>();

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