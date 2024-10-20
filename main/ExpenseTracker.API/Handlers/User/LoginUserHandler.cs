using ExpenseTracker.API.DTOs.User;
using ExpenseTracker.Infrastructure.Abstractions;
using ExpenseTracker.Infrastructure.Abstractions.Auth;
using MediatR;

namespace ExpenseTracker.API.Requests.Commands.User;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, LoginUserCommandResponse>
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    public LoginUserHandler(IUserService userService, ITokenService tokenService)
    {
        this._userService = userService;
        this._tokenService = tokenService;
    }
    public async Task<LoginUserCommandResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.LoginAsync(request.Password, request.Password);

        if (user == null || user.Id == Guid.Empty)
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        return new LoginUserCommandResponse { Token = _tokenService.GenerateToken(user) };
    }
}