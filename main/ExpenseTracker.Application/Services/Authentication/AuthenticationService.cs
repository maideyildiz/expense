
using ExpenseTracker.Application.Common.Interfaces.Authentication;

namespace ExpenseTracker.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    public Task<AuthenticationResult> LoginAsync(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task<AuthenticationResult> RegisterAsync(string name, string surname, string email, string password)
    {
        //check if user exists

        //create user

        //create token

        //return token
        Guid id = Guid.NewGuid();
        var token = _jwtTokenGenerator.GenerateToken(id, name, surname);
        throw new NotImplementedException();
    }
}