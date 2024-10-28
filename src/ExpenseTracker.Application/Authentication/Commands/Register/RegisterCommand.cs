using ErrorOr;
using ExpenseTracker.Application.Authentication.Common;
using MediatR;

namespace ExpenseTracker.Application.Authentication.Commands.Register;

public record RegisterCommand(string Name, string Surname, string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;