using ExpenseTracker.Application.Common.Base;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ErrorOr;
using MediatR;

namespace ExpenseTracker.Application.UserOperations.Commands.Logout;

public class LogoutCommandHandler : BaseHandler, IRequestHandler<LogoutCommand, ErrorOr<bool>>
{
    private readonly IUserService _userService;
    public LogoutCommandHandler(IUserService userService)
    : base(userService)
    {
        _userService = userService;
    }

    public async Task<ErrorOr<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var userIdResult = GetUserId();
        if (userIdResult.IsError)
        {
            return userIdResult.Errors;
        }
        return await _userService.LogoutUserAsync(userIdResult.Value);
    }
}
