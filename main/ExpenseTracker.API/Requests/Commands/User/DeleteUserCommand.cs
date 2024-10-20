using ExpenseTracker.API.DTOs.User;
using MediatR;

namespace ExpenseTracker.API.Requests.Commands.User;

public record DeleteUserCommand(DeleteUserCommandRequest Request) : IRequest<bool>;