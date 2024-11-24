namespace ExpenseTracker.Infrastructure.Services;
using ExpenseTracker.Application.Common.Interfaces.Services;
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTime Now => DateTime.Now;
    public DateTime GetStartOfMonth(DateTime date)
    {
        return new DateTime(date.Year, date.Month, 1);
    }
    public DateTime GetEndOfMonth(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int lastDay = DateTime.DaysInMonth(year, month);
        return new DateTime(year, month, lastDay, 23, 59, 59);
    }

    public DateTime AddMonths(DateTime date, int months)
    {
        return date.AddMonths(months);
    }

    public DateTime AddDays(DateTime date, int days)
    {
        return date.AddDays(days);
    }
}