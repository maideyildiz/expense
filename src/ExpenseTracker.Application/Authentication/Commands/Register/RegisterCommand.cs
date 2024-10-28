namespace ExpenseTracker.Application.Authentication.Commands.Register;
using ErrorOr;
using MediatR;
using ExpenseTracker.Application.Authentication.Common;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password)
    : IRequest<ErrorOr<AuthenticationResult>>;