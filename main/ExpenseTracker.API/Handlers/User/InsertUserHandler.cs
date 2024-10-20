using ExpenseTracker.API.DTOs.User;
using ExpenseTracker.API.Requests.Commands.User;
using ExpenseTracker.Infrastructure.Abstractions;
using Mapster;
using MediatR;

namespace ExpenseTracker.API.Handlers.User;

public class InsertUserHandler : IRequestHandler<InsertUserCommand, InsertUserCommandResult>
{
    private readonly IUserService _userService;
    public InsertUserHandler(IUserService userService)
    {
        this._userService = userService;
    }
    public async Task<InsertUserCommandResult> Handle(InsertUserCommand request, CancellationToken cancellationToken)
    {
        Core.Models.User user = request.Adapt<Core.Models.User>();
        var result = await _userService.RegisterAsync(user);
        return result.Adapt<InsertUserCommandResult>();
    }
}