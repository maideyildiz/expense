using ExpenseTracker.Infrastructure.Logging.Services;

using FluentMigrator.Runner;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExpenseTracker.Infrastructure.Database.Extensions;

public static class MigrationManager
{
    public static IHost MigrateDatabase(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogService>();
            try
            {
                migrationService.ListMigrations();
                migrationService.MigrateUp();
            }
            catch (Exception ex)
            {
                logger.LogError("Migration failed", null, ex);
                throw;
            }
        }
        return host;
    }
}

