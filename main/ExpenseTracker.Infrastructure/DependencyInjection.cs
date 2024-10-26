using ExpenseTracker.Application.Common.Interfaces.Authentication;
using ExpenseTracker.Application.Common.Interfaces.Persistence;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Infrastructure.Authentication;
using ExpenseTracker.Infrastructure.Persistence;
using ExpenseTracker.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
    ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IDbRepository, DbRepository>();
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        //services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}