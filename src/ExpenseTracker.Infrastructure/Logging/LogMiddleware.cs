using ExpenseTracker.Infrastructure.Logging.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.Infrastructure.Logging;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogService _logger;

    public LoggingMiddleware(RequestDelegate next, ILogService logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            _logger.LogInformation($"Handling request: {context.Request.Path}");
            await _next(context);
            _logger.LogInformation($"Finished handling request.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error handling request: {ex.Message}", null, ex.ToString());
            throw;
        }
    }
}
