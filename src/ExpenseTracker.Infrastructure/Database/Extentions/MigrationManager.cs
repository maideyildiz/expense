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
            try
            {
                migrationService.ListMigrations();
                migrationService.MigrateUp();
            }
            catch
            {
                //log errors or ...
                throw;
            }
        }
        return host;
    }
}