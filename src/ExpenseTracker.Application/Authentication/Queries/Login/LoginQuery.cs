using ErrorOr;
using ExpenseTracker.Application.Authentication.Common;
using MediatR;

namespace ExpenseTracker.Application.Authentication.Queries.Login;

public record LoginQuery(string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;