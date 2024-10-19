using ExpenseTracker.API.DTOs;
using MediatR;

namespace ExpenseTracker.API.Requests.Commands;

public record DeleteUserCommand(DeleteUserCommandRequest Request) : IRequest<bool>;