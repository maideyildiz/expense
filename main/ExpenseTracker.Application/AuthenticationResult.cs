namespace ExpenseTracker.Application;

public record AuthenticationResult(
    Guid Id,
    string Name,
    string Surname,
    string Email,
    string Token);