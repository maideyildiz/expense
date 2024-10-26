using ErrorOr;
using ExpenseTracker.Application.Common.Interfaces.Authentication;
using ExpenseTracker.Application.Common.Interfaces.Persistence;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Common.Errors;
using MediatR;

namespace ExpenseTracker.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }


    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(command.Email);
        if (user != null)
        {
            return Errors.User.DublicateEmail;
        }
        //create user
        var newUser = new User
        {
            Name = command.Name,
            Surname = command.Surname,
            Email = command.Email,
            Password = command.Password
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
}