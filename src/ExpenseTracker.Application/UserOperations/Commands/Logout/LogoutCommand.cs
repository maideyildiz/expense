using MediatR;
using ErrorOr;
namespace ExpenseTracker.Application.UserOperations.Commands.Logout;

public record LogoutCommand() : IRequest<ErrorOr<bool>>;