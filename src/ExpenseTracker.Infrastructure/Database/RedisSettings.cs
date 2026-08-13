namespace ExpenseTracker.Infrastructure.Database;

public class RedisSettings
{
    public const string SectionName = "Redis";
    public bool Enabled { get; init; }
    public string ConnectionString { get; init; } = null!;
    public string InstanceName { get; init; } = null!;
}
