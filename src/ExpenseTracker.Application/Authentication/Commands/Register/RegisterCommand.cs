namespace ExpenseTracker.Application.Authentication.Commands.Register;
using ErrorOr;

using MediatR;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    Guid CityId,
    Guid SubscriptionId)
    : IRequest<ErrorOr<string>>;