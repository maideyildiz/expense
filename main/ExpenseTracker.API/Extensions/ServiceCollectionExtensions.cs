using System.Data;
using System.Reflection;
using ExpenseTracker.Infrastructure.Abstractions;
using ExpenseTracker.Infrastructure.Abstractions.Auth;
using ExpenseTracker.Infrastructure.Services;
using ExpenseTracker.Infrastructure.Services.Auth;
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

        // Servisleri ekle
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));


        return services;
    }

}
