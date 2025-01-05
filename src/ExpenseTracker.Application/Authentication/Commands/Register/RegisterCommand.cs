namespace ExpenseTracker.Application.Authentication.Commands.Register;
using ErrorOr;

using MediatR;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Username,
    string Password,
    Guid CityId,
    decimal? MonthlySalary = decimal.Zero,
    decimal? YearlySalary = decimal.Zero)
    : IRequest<ErrorOr<string>>;