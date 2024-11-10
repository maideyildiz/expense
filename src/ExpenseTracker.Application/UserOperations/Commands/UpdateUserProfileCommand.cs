using ErrorOr;

using MediatR;

namespace ExpenseTracker.Application.UserOperations.Commands;

public record UpdateUserProfileCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    decimal MonthlySalary,
    decimal YearlySalary,
    bool IsActive,
    Guid CityId) : IRequest<ErrorOr<int>>;