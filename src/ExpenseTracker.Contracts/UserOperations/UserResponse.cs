namespace ExpenseTracker.Contracts.UserOperations;

public record UserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    decimal MonthlySalary,
    decimal YearlySalary,
    string CityName);