using Microsoft.Extensions.Configuration;
namespace ExpenseTracker.Infrastructure.Data.DbSettings;
public class DbOptions
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }

    public DbOptions(IConfiguration configuration)
    {
        ConnectionString = configuration["DbSettings:ConnectionString"] ?? throw new ArgumentNullException("ConnectionString cannot be null.");
        DatabaseName = configuration["DbSettings:DatabaseName"] ?? throw new ArgumentNullException("DatabaseName cannot be null.");

    }
}
