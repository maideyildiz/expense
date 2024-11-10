using ExpenseTracker.Application.Common.Interfaces.Authentication;
using ExpenseTracker.Application.Common.Interfaces.Persistence;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Infrastructure.Helpers;
using ExpenseTracker.Application.Authentication.Commands.Register;
using ExpenseTracker.Application.Common.Errors;
using ErrorOr;
using ExpenseTracker.Application.Authentication.Queries.Login;
using ExpenseTracker.Core.Entities;
namespace ExpenseTracker.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public UserService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<string>> LoginUserAsync(LoginQuery query)
    {
        var user = await this._userRepository.GetUserByEmailAsync(query.Email);

        if (user is null || !user.IsActive || !PasswordHasher.VerifyPassword(query.Password, user.PasswordHash))
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = this._jwtTokenGenerator.GenerateToken(user);

        return token;
    }

    public async Task<ErrorOr<string>> RegisterUserAsync(RegisterCommand command)
    {
        var existingUser = await _userRepository.GetUserByEmailAsync(command.Email);
        if (existingUser != null)
        {
            return Errors.Authentication.InvalidCredentials;
        }
        var passwordHash = PasswordHasher.HashPassword(command.Password);
        var newUser = User.Create(command.FirstName, command.LastName, command.Email, passwordHash, command.CityId);

        await _userRepository.AddUserAsync(newUser);

        var token = _jwtTokenGenerator.GenerateToken(newUser);

        return token;
    }

}