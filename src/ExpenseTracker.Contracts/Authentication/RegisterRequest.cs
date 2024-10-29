namespace ExpenseTracker.Contracts.Authentication;
public record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    Guid CityId,
    Guid SubscriptionId
);