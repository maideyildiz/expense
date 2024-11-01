namespace ExpenseTracker.Infrastructure.Logging.Services;

public interface ILogService
{
    void LogTrace(string message, string? userId = null, string? exception = null);
    void LogDebug(string message, string? userId = null, string? exception = null);
    void LogInformation(string message, string? userId = null, string? exception = null);
    void LogWarning(string message, string? userId = null, string? exception = null);
    void LogError(string message, string? userId = null, string? exception = null);
    void LogCritical(string message, string? userId = null, string? exception = null);
}
