using ErrorOr;
using ExpenseTracker.Application.Common.Interfaces.Authentication;
using ExpenseTracker.Application.Common.Interfaces.Persistence;
using MediatR;
using ExpenseTracker.Core.Common.Errors;
using ExpenseTracker.Application.Authentication.Common;

namespace ExpenseTracker.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(query.Email);
        if (user == null)
        {
            return Errors.Authentication.InvalidCredentials;
        }
        if (user.PasswordHash != query.Password)
        {
            return Errors.Authentication.InvalidCredentials; ;
        }

        var token = _jwtTokenGenerator.GenerateToken(new Guid(), user.FirstName, user.LastName);

        return new AuthenticationResult(
            user,
            token);
    }
}