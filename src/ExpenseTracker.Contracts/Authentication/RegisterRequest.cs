namespace ExpenseTracker.Contracts.Authentication;
public record RegisterRequest(
    string Email,
    string Name,
    string Surname,
    string Password

);