using ExpenseTracker.API.DTOs;
using MediatR;

namespace ExpenseTracker.API.Requests.Commands;

public record InsertUserCommand(InsertUserCommandRequest Request) : IRequest<InsertUserCommandResult>;