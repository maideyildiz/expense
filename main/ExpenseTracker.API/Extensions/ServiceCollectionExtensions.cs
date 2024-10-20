using System.Data;
using System.Reflection;
using ExpenseTracker.Infrastructure.Abstractions;
using ExpenseTracker.Infrastructure.Services;
using ExpenseTracker.Infrastructure.Abstractions.Auth;
using MySqlConnector;
using ExpenseTracker.Infrastructure.Services.Auth;

namespace ExpenseTracker.API.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProjectDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // MediatR'ı ekle
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        // MySQL bağlantısını ekle
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddTransient<IDbConnection>(sp => new MySqlConnection(connectionString));

        // Servisleri ekle
        services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
        //services.AddScoped(typeof(ITokenService), typeof(TokenService));

        return services;
    }
}