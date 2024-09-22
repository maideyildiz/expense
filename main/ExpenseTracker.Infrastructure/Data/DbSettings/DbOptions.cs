using Microsoft.Extensions.Configuration;
namespace ExpenseTracker.Infrastructure.Data.DbSettings;
public class DbOptions
{
    public required string ConnectionString { get; set; }
    public required string DatabaseName { get; set; }
    public required string CollectionName { get; set; }

    public DbOptions(IConfiguration configuration)
    {
        ConnectionString = configuration["DbSettings:ConnectionString"] ?? throw new ArgumentNullException("ConnectionString cannot be null.");
        DatabaseName = configuration["DbSettings:DatabaseName"] ?? throw new ArgumentNullException("DatabaseName cannot be null.");
        CollectionName = configuration["DbSettings:CollectionName"] ?? throw new ArgumentNullException("CollectionName cannot be null.");
    }
}
