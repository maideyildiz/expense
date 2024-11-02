using ExpenseTracker.Application.Authentication.Common;
using ExpenseTracker.Application.Common.Interfaces.Authentication;
using ExpenseTracker.Application.Common.Interfaces.Persistence;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Infrastructure.Helpers;
using ExpenseTracker.Application.Authentication.Commands.Register;
using ExpenseTracker.Core.UserAggregate;
using ExpenseTracker.Application.Common.Errors;
using ErrorOr;
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
    public async Task<ErrorOr<AuthenticationResult>> RegisterUserAsync(RegisterCommand command)
    {
        var existingUser = await _userRepository.GetUserByEmailAsync(command.Email);
        if (existingUser != null)
        {
            return Errors.Authentication.InvalidCredentials;
        }
        var passwordHash = PasswordHasher.HashPassword(command.Password);
        var newUser = User.Create(command.FirstName, command.LastName, command.Email, passwordHash, command.SubscriptionId, command.CityId);

        await _userRepository.AddUserAsync(newUser);

        var token = _jwtTokenGenerator.GenerateToken(newUser);

        return new AuthenticationResult(token);
    }

}