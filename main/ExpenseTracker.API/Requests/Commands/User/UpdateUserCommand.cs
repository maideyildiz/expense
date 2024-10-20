using ExpenseTracker.API.DTOs.User;
using MediatR;

namespace ExpenseTracker.API.Requests.Commands.User;

public record UpdateUserCommand(UpdateUserCommandRequest Request) : IRequest<UpdateUserCommandResult>;