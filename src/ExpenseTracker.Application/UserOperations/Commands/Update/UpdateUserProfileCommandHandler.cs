using MediatR;
using ErrorOr;
using ExpenseTracker.Application.Common.Base;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.UserOperations.Common;
namespace ExpenseTracker.Application.UserOperations.Commands.Update;

public class UpdateUserProfileCommandHandler : BaseHandler, IRequestHandler<UpdateUserProfileCommand, ErrorOr<UserResult>>
{
    private readonly IUserService _userService;
    public UpdateUserProfileCommandHandler(IUserService userService)
    : base(userService)
    {
        _userService = userService;
    }

    public async Task<ErrorOr<UserResult>> Handle(UpdateUserProfileCommand command, CancellationToken cancellationToken)
    {
        var userIdResult = GetUserId();
        if (userIdResult.IsError)
        {
            return userIdResult.Errors;
        }
        return await _userService.UpdateUserDetailsAsync(command, userIdResult.Value);
    }
}
