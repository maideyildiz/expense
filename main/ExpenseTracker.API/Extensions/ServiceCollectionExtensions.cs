using System.Data;
using System.Reflection;
using System.Text;
using ExpenseTracker.API.Mappings;
using ExpenseTracker.Infrastructure.Abstractions;
using ExpenseTracker.Infrastructure.Abstractions.Auth;
using ExpenseTracker.Infrastructure.Services;
using ExpenseTracker.Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MySqlConnector;

namespace ExpenseTracker.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProjectDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // MediatR'ı ekle
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        // MySQL bağlantısını ekle
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException("The connection string 'DefaultConnection' is not configured.");
        }

        // Register IDatabaseConnection with the connection string
        services.AddScoped<IDatabaseConnection>(sp => new DatabaseConnection(connectionString));

        // Register IDbConnection
        services.AddTransient<IDbConnection>(sp => new MySqlConnection(connectionString));

        var jwtKey = configuration["JwtSettings:SecretKey"];
        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new ArgumentException("JWT key is not configured.");
        }

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    ClockSkew = TimeSpan.Zero // Token süresi hassasiyeti için
                };
            });

        MapsterConfiguration.RegisterMappings();
        // Servisleri ekle
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));


        return services;
    }

}
