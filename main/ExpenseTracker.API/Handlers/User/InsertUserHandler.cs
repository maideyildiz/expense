using ExpenseTracker.API.Requests.Commands.User;
using ExpenseTracker.Infrastructure.Abstractions;
using ExpenseTracker.Infrastructure.Abstractions.Auth;
using Mapster;
using MediatR;

namespace ExpenseTracker.API.Handlers.User;

public class InsertUserHandler : IRequestHandler<InsertUserCommand, string>
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    public InsertUserHandler(IUserService userService, ITokenService tokenService)
    {
        this._userService = userService;
        this._tokenService = tokenService;
    }
    public async Task<string> Handle(InsertUserCommand request, CancellationToken cancellationToken)
    {
        string token = string.Empty;
        var user = request.Request.Adapt<Core.Models.User>();
        var result = await _userService.RegisterAsync(user);

        if (result == null)
            throw new InvalidOperationException("User already exists.");

        else
        {
            token = _tokenService.GenerateToken(result);
            if (token == null)
            {
                throw new InvalidOperationException("Token is null.");
            }
        }

        return token;
    }
}