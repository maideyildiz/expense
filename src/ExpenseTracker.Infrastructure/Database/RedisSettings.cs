namespace ExpenseTracker.Infrastructure.Database;

public class RedisSettings
{
    public const string SectionName = "Redis";
    public string ConnectionString { get; init; } = null!;
    public string InstanceName { get; init; } = null!;
}