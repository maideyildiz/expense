using MediatR;
using ErrorOr;
namespace ExpenseTracker.Application.Authentication.Commands.Logout;

public record LogoutCommand() : IRequest<ErrorOr<bool>>;