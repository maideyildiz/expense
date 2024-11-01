using Dapper;
using Serilog;
using ExpenseTracker.Application.Common.Interfaces.Persistence;
using ExpenseTracker.Infrastructure.Logging;

using Microsoft.Extensions.Logging;
namespace ExpenseTracker.Infrastructure.Logging.Services;

public class LogService : ILogService
{
    public void LogCritical(string message, string? userId = null, string? exception = null)
    {
        Log.Fatal($"message:{message} userId:{userId} exception:{exception}");
    }

    public void LogDebug(string message, string? userId = null, string? exception = null)
    {
        Log.Debug($"message:{message} userId:{userId} exception:{exception}");
    }

    public void LogError(string message, string? userId = null, string? exception = null)
    {
        Log.Error($"message:{message} userId:{userId} exception:{exception}");
    }

    public void LogInformation(string message, string? userId = null, string? exception = null)
    {
        Log.Information($"message:{message} userId:{userId} exception:{exception}");
    }

    public void LogTrace(string message, string? userId = null, string? exception = null)
    {
        Log.Verbose($"message:{message} userId:{userId} exception:{exception}");
    }

    public void LogWarning(string message, string? userId = null, string? exception = null)
    {
        Log.Warning($"message:{message} userId:{userId} exception:{exception}");
    }
}
