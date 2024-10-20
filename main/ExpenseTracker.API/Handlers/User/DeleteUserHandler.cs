using ExpenseTracker.API.Requests.Commands.User;
using ExpenseTracker.Infrastructure.Abstractions;
using MediatR;

namespace ExpenseTracker.API.Handlers.User;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserService _userService;

    public DeleteUserHandler(IUserService userService)
    {
        this._userService = userService;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _userService.DeleteAsync(request.Request.Id);
        return result == 1;
    }
}