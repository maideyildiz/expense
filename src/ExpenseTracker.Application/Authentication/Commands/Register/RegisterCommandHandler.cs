using ErrorOr;
using ExpenseTracker.Application.Common.Interfaces.Authentication;
using ExpenseTracker.Application.Common.Interfaces.Persistence;
using ExpenseTracker.Core.Common.Errors;
using MediatR;
using ExpenseTracker.Application.Authentication.Common;
using ExpenseTracker.Core.UserAggregate;
using ExpenseTracker.Core.UserAggregate.Entities;

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
        // var newUser = User.Create(
        //     command.Name,
        //     command.Surname,
        //     command.Email,
        //     command.Password,
        //     0,
        //     0,
        //     DateTime.Now,
        //     DateTime.Now,
        //     DateTime.Now,
        //     true
        // //new Subscription("Free", "Free", 0)
        // );
        //var newUser = new User.Create(command.Name, command.Surname, command.Email, command.Password, 0, 0, DateTime.Now, DateTime.Now, DateTime.Now, true, new Subscription("Free", "Free", 0)).Value;
        // {
        //     FirstName = command.Name,
        //     LastName = command.Surname,
        //     Email = command.Email,
        //     PasswordHash = command.Password
        // };

        //await _userRepository.AddUserAsync(newUser);
        //create token

        //return token
        var token = string.Empty;
        return new AuthenticationResult(user,
            token);

    }
}