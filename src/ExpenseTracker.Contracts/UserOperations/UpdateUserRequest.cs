namespace ExpenseTracker.Contracts.UserOperations;

public record UpdateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    decimal MonthlySalary,
    decimal YearlySalary,
    bool IsActive,
    Guid CityId);