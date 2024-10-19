using ExpenseTracker.API.DTOs;
using MediatR;

namespace ExpenseTracker.API.Requests.Commands;

public record UpdateUserCommand(UpdateUserCommandRequest Request) : IRequest<UpdateUserCommandResult>;