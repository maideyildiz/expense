namespace ExpenseTracker.Infrastructure.Services;
using ExpenseTracker.Application.Common.Interfaces.Services;
/// <summary>
/// Class providing the current UTC date and time.
/// </summary>
public class DateTimeProvider : IDateTimeProvider
{
    /// <summary>
    /// Gets the current UTC date and time.
    /// </summary>
    public DateTime UtcNow => DateTime.UtcNow;
}