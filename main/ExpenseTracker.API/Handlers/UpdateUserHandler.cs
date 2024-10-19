using ExpenseTracker.API.DTOs;
using ExpenseTracker.API.Requests.Commands;
using ExpenseTracker.Core.Models;
using ExpenseTracker.Infrastructure.Abstractions;
using Mapster;
using MediatR;

namespace ExpenseTracker.API.Handlers;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserCommandResult>
{
    private readonly IUserService _userService;

    public UpdateUserHandler(IUserService userService)
    {
        this._userService = userService;
    }

    public async Task<UpdateUserCommandResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User user = request.Adapt<User>();
        var result = await _userService.UpdateAsync(user);
        return result.Adapt<UpdateUserCommandResult>();
        throw new NotImplementedException();
    }
}