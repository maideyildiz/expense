namespace ExpenseTracker.Infrastructure.Database;

public class DatabaseSettings
{
    public const string SectionName = "DatabaseSettings";
    public string DefaultConnection { get; init; } = null!;
}