namespace ExpenseTracker.Contracts.Authentication;
public record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Username,
    string Password,
    Guid CityId
);