using ExpenseTracker.Infrastructure.Logging.Enums;

namespace ExpenseTracker.Infrastructure.Logging;

public class Log
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
    public LogType LogType { get; }
    public string? UserId { get; set; }
    public string? Exception { get; set; }

    public static Log Create(string? message, DateTime? timestamp = null, LogType? logType = null, string? userId = null, string? exception = null)
    {
        return new Log(
            Guid.NewGuid(),
            message ?? "Default",
            timestamp ?? DateTime.UtcNow,
            logType ?? LogType.Debug,
            string.IsNullOrWhiteSpace(userId) ? Guid.Empty.ToString() : userId,
            string.IsNullOrWhiteSpace(exception) ? "No Exception" : exception);
    }

    private Log(Guid id, string message, DateTime timestamp, LogType logType, string? userId = null, string? exception = null)
    {
        this.Message = message;
        this.Timestamp = timestamp;
        this.LogType = logType;
        this.UserId = userId;
        this.Exception = exception;
    }
}