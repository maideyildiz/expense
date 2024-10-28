

namespace ExpenseTracker.Application.Authentication.Queries.Login;
using ErrorOr;
using ExpenseTracker.Application.Common.Interfaces.Authentication;
using ExpenseTracker.Application.Common.Interfaces.Persistence;
using MediatR;
using ExpenseTracker.Core.Common.Errors;
using ExpenseTracker.Application.Authentication.Common;
public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly IUserRepository userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        this.jwtTokenGenerator = jwtTokenGenerator;
        this.userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByEmailAsync(query.Email);
        if (user == null)
        {
            return Errors.Authentication.InvalidCredentials;
        }
        if (user.PasswordHash != query.Password)
        {
            return Errors.Authentication.InvalidCredentials; ;
        }

        var token = jwtTokenGenerator.GenerateToken(new Guid(), user.FirstName, user.LastName);

        return new AuthenticationResult(
            user,
            token);
    }
}