namespace ExpenseTracker.Infrastructure;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ExpenseTracker.Application.Common.Interfaces.Authentication;
using ExpenseTracker.Application.Common.Interfaces.Persistence;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Infrastructure.Authentication;
using ExpenseTracker.Infrastructure.Persistence;
using ExpenseTracker.Infrastructure.Services;
using ExpenseTracker.Infrastructure.Database;
using FluentMigrator.Runner.Logging;
using FluentMigrator.Runner;
using System.Reflection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddDbConnection(configuration);
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddAuth(configuration);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }

    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                });

        return services;
    }

    public static IServiceCollection AddDbConnection(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var dbSettings = new DatabaseSettings();
        configuration.Bind(DatabaseSettings.SectionName, dbSettings);
        services.AddSingleton(Options.Create(dbSettings));
        services.AddScoped<IDbConnectionFactory>(provider => new MySqlConnectionFactory(dbSettings.DefaultConnection));

        services.AddLogging(c => c.AddFluentMigratorConsole())
            .AddFluentMigratorCore()
            .ConfigureRunner(c => c.AddMySql5()
                .WithGlobalConnectionString(dbSettings.DefaultConnection)
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());

        services.AddScoped<IDbRepository, DbRepository>();

        return services;
    }
}