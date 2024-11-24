namespace ExpenseTracker.Application.Common.Interfaces.Services;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
    DateTime Now { get; }
    DateTime GetStartOfMonth(DateTime date);
    DateTime GetEndOfMonth(DateTime date);
    DateTime AddMonths(DateTime date, int months);
    DateTime AddDays(DateTime date, int days);
}