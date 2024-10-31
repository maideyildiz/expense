using Dapper;

using ExpenseTracker.Application.Common.Interfaces.Persistence;
using ExpenseTracker.Infrastructure.Logging;
using ExpenseTracker.Infrastructure.Logging.Enums;

using Microsoft.Extensions.Logging;
namespace ExpenseTracker.Infrastructure.Logging.Services;

public class LogService : ILogService
{
    private readonly ILogger<LogService> _logger;
    public LogService(ILogger<LogService> logger)
    {
        _logger = logger;
    }
    public void LogCritical(string message, string? userId = null, string? exception = null)
    {
        _logger.LogCritical($"message:{message} userId:{userId} exception:{exception}");
    }

    public void LogDebug(string message, string? userId = null, string? exception = null)
    {
        _logger.LogDebug($"message:{message} userId:{userId} exception:{exception}");
    }

    public void LogError(string message, string? userId = null, string? exception = null)
    {
        _logger.LogError($"message:{message} userId:{userId} exception:{exception}");
    }

    public void LogInformation(string message, string? userId = null, string? exception = null)
    {
        _logger.LogInformation($"message:{message} userId:{userId} exception:{exception}");
    }

    public void LogTrace(string message, string? userId = null, string? exception = null)
    {
        _logger.LogTrace($"message:{message} userId:{userId} exception:{exception}");
    }

    public void LogWarning(string message, string? userId = null, string? exception = null)
    {
        _logger.LogWarning($"message:{message} userId:{userId} exception:{exception}");
    }
}