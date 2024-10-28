
namespace ExpenseTracker.Application.Authentication.Commands.Register;
using ErrorOr;
using MediatR;
using ExpenseTracker.Application.Authentication.Common;
using ExpenseTracker.Application.Common.Interfaces.Authentication;
using ExpenseTracker.Application.Common.Interfaces.Persistence;
using ExpenseTracker.Core.Common.Errors;


public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly IUserRepository userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        this.jwtTokenGenerator = jwtTokenGenerator;
        this.userRepository = userRepository;
    }


    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByEmailAsync(command.Email);
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