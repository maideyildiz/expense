using ExpenseTracker.API.DTOs.User;
using MediatR;

namespace ExpenseTracker.API.Requests.Commands.User;

public record InsertUserCommand(InsertUserCommandRequest Request) : IRequest<string>;