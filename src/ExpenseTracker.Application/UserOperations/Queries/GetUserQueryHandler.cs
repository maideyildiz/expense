using MediatR;
using ErrorOr;
using ExpenseTracker.Application.Common.Base;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.UserOperations.Common;
namespace ExpenseTracker.Application.UserOperations.Queries;

public class GetUserQueryHandler : BaseHandler, IRequestHandler<GetUserProfileQuery, ErrorOr<UserResult>>
{
    private readonly IUserService _userService;
    public GetUserQueryHandler(IUserService userService)
    : base(userService)
    {
        _userService = userService;
    }

    public async Task<ErrorOr<UserResult>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var userIdResult = GetUserId();
        if (userIdResult.IsError)
        {
            return userIdResult.Errors;
        }
        return await _userService.GetUserDetailsAsync(userIdResult.Value);
    }
}
