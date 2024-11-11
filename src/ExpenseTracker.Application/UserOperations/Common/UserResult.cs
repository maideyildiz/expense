namespace ExpenseTracker.Application.UserOperations.Common;

public record UserResult(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    decimal MonthlySalary,
    decimal YearlySalary,
    string CityName);