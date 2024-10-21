
using ErrorOr;
using ExpenseTracker.Application.Common.Interfaces.Authentication;
using ExpenseTracker.Application.Common.Interfaces.Persistence;
using ExpenseTracker.Core.Common.Errors;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public async Task<ErrorOr<AuthenticationResult>> RegisterAsync(string name, string surname, string email, string password)
    {
        //check if user exists
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user != null)
        {
            return Errors.User.DublicateEmail;
        }
        //create user
        var newUser = new User
        {
            Name = name,
            Surname = surname,
            Email = email,
            Password = password
        };

        await _userRepository.AddUserAsync(newUser);
        //create token

        //return token
        var token = _jwtTokenGenerator.GenerateToken(newUser.Id, newUser.Name, newUser.Surname);
        return new AuthenticationResult(
            newUser.Id,
            newUser.Name,
            newUser.Surname,
            newUser.Email,
            token);
    }

    public async Task<ErrorOr<AuthenticationResult>> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null)
        {
            return Errors.User.UserNotFound;
        }
        if (user.Password != password)
        {
            return Errors.User.CredentialsError;
        }

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Name, user.Surname);

        return new AuthenticationResult(
            user.Id,
            user.Name,
            user.Surname,
            user.Email,
            token);
    }
}