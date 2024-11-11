using ErrorOr;

using ExpenseTracker.Application.Common.Interfaces.Services;

namespace ExpenseTracker.Application.Common.Base;
public abstract class BaseHandler
{
    private readonly IUserService _userService;

    protected BaseHandler(IUserService userService)
    {
        _userService = userService;
    }

    protected ErrorOr<Guid> GetUserId()
    {
        var userId = _userService.GetUserId();
        if (userId.IsError)
        {
            return userId.Errors;
        }
        return userId.Value;
    }
}
