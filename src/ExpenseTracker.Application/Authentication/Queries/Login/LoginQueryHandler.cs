

namespace ExpenseTracker.Application.Authentication.Queries.Login;
using ErrorOr;
using ExpenseTracker.Application.Common.Interfaces.Authentication;
using ExpenseTracker.Application.Common.Interfaces.Persistence;
using MediatR;
using ExpenseTracker.Core.Common.Errors;
using ExpenseTracker.Application.Authentication.Common;
public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        this._jwtTokenGenerator = jwtTokenGenerator;
        this._userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var user = await this._userRepository.GetUserByEmailAsync(query.Email);
        if (user is null || user.PasswordHash != query.Password || !user.IsActive)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = this._jwtTokenGenerator.GenerateToken(user.Id.Value, user.FirstName, user.LastName, user.Subscription.Name);

        return new AuthenticationResult(
            user,
            token);
    }
}