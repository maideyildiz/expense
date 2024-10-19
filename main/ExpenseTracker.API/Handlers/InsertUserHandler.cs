using ExpenseTracker.API.DTOs;
using ExpenseTracker.API.Requests.Commands;
using ExpenseTracker.Core.Models;
using ExpenseTracker.Infrastructure.Abstractions;
using Mapster;
using MediatR;

namespace ExpenseTracker.API.Handlers;

public class InsertUserHandler : IRequestHandler<InsertUserCommand, InsertUserCommandResult>
{
    private readonly IUserService _userService;
    public InsertUserHandler(IUserService userService)
    {
        this._userService = userService;
    }
    public async Task<InsertUserCommandResult> Handle(InsertUserCommand request, CancellationToken cancellationToken)
    {
        User user = request.Adapt<User>();
        var result = await _userService.RegisterAsync(user);
        return result.Adapt<InsertUserCommandResult>();
    }
}