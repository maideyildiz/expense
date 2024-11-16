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
using Serilog;
using Serilog.Sinks.MariaDB.Extensions;
using ExpenseTracker.Infrastructure.Logging.Services;
using ExpenseTracker.Infrastructure.Persistence.Repositories;
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Core.Entities;
using Dapper;
using ExpenseTracker.Infrastructure.Cache;
using StackExchange.Redis;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddDbConnection(configuration);
        services.AddRedis(configuration);
        services.AddCustomLogging(configuration);
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IInvestmentCategoryRepository, InvestmentCategoryRepository>();
        services.AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IInvestmentRepository, InvestmentRepository>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IExpenseService, ExpenseService>();
        services.AddScoped<IInvestmentService, InvestmentService>();
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

    // Custom logging method to avoid confusion with built-in logging
    public static IServiceCollection AddCustomLogging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ILogService, LogService>();

        var dbSettings = new DatabaseSettings();
        configuration.Bind(DatabaseSettings.SectionName, dbSettings);

        Serilog.Log.Logger = new LoggerConfiguration()
            .WriteTo.MariaDB(
                connectionString: dbSettings.DefaultConnection,
                tableName: "Logs",
                autoCreateTable: true).CreateLogger();

        services.AddSingleton(Serilog.Log.Logger);

        return services;
    }

    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. RedisSettings'i yapılandır ve valide et
        var redisSettings = new RedisSettings();
        configuration.Bind(RedisSettings.SectionName, redisSettings);

        if (string.IsNullOrWhiteSpace(redisSettings.ConnectionString))
        {
            throw new ArgumentException("Redis connection string is not configured.");
        }

        // 2. RedisSettings'i DI konteynerine ekle
        services.AddSingleton(redisSettings);

        // 3. StackExchange.Redis cache servisini ekle
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisSettings.ConnectionString;
            options.InstanceName = redisSettings.InstanceName;
        });

        // 4. ConnectionMultiplexer'i singleton olarak ekle
        var connectionMultiplexer = ConnectionMultiplexer.Connect(redisSettings.ConnectionString);
        services.AddSingleton<IConnectionMultiplexer>(connectionMultiplexer);

        // 5. ICacheService implementasyonu olarak RedisCacheService'i scoped ekle
        services.AddScoped<ICacheService, RedisCacheService>();

        return services;
    }
}
