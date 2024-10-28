namespace ExpenseTracker.Infrastructure.Services;
using ExpenseTracker.Application.Common.Interfaces.Services;
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}