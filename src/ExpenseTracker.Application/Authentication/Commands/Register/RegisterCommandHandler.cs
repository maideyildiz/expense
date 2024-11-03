using ErrorOr;

using ExpenseTracker.Application.Common.Interfaces.Services;

using MediatR;
namespace ExpenseTracker.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<string>>
{
    private readonly IUserService _userService;

    public RegisterCommandHandler(
        IUserService userService)
    {
        this._userService = userService;
    }

    public async Task<ErrorOr<string>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var result = await _userService.RegisterUserAsync(command);

        return result;
    }
}