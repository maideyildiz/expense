using ErrorOr;
using ExpenseTracker.Application.Common.Interfaces.Authentication;
using ExpenseTracker.Application.Common.Interfaces.Persistence;
using MediatR;
using ExpenseTracker.Application.Common.Errors;
using ExpenseTracker.Application.Common.Interfaces.Services;
namespace ExpenseTracker.Application.Authentication.Queries.Login;
public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<string>>
{
    private readonly IUserService _userService;

    public LoginQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<ErrorOr<string>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        return await _userService.LoginUserAsync(query);
    }
}