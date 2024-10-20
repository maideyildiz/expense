using ExpenseTracker.API.DTOs.User;
using MediatR;

namespace ExpenseTracker.API.Requests.Commands.User;

public record LoginUserCommand(string Email, string Password) : IRequest<LoginUserCommandResponse>;