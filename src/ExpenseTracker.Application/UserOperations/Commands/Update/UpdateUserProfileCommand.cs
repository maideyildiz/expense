using ErrorOr;
using MediatR;
using ExpenseTracker.Application.UserOperations.Common;

namespace ExpenseTracker.Application.UserOperations.Commands.Update;

public record UpdateUserProfileCommand(
    string? FirstName,
    string? LastName,
    string? Email,
    string? Password,
    decimal? MonthlySalary,
    decimal? YearlySalary,
    bool? IsActive,
    Guid? CityId) : IRequest<ErrorOr<UserResult>>;