using ExpenseTracker.API.DTOs.User;
using ExpenseTracker.API.Requests.Commands.User;
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
        Core.Models.User user = request.Adapt<Core.Models.User>();
        var result = await _userService.UpdateAsync(user);
        return result.Adapt<UpdateUserCommandResult>();
        throw new NotImplementedException();
    }
}