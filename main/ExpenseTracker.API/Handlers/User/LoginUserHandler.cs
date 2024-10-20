using ExpenseTracker.API.DTOs.User;
using ExpenseTracker.Infrastructure.Abstractions;
using MediatR;

namespace ExpenseTracker.API.Requests.Commands.User;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, LoginUserCommandResponse>
{
    private readonly IUserService _userService;
    public LoginUserHandler(IUserService userService)
    {
        this._userService = userService;
    }
    public async Task<LoginUserCommandResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var userToken = await _userService.LoginAsync(request.Password, request.Password);

        if (userToken == null || string.IsNullOrEmpty(userToken))
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        return new LoginUserCommandResponse { Token = userToken };
    }
}