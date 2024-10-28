
namespace ExpenseTracker.Application.Authentication.Commands.Register;
using ErrorOr;
using MediatR;
using ExpenseTracker.Application.Authentication.Common;
using ExpenseTracker.Application.Common.Interfaces.Authentication;
using ExpenseTracker.Application.Common.Interfaces.Persistence;
using ExpenseTracker.Core.Common.Errors;
using ExpenseTracker.Core.UserAggregate;
using ExpenseTracker.Core.UserAggregate.Entities;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        this._jwtTokenGenerator = jwtTokenGenerator;
        this._userRepository = userRepository;
    }


    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var user = await this._userRepository.GetUserByEmailAsync(command.Email);
        if (user is not null)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        Subscription subs = Subscription.Create("Free", "Free", decimal.One);
        //create user
        var newUser = User.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            command.Password,
            0,
            0,
            DateTime.Now,
            DateTime.Now,
            DateTime.Now,
            true,
            subs);


        await this._userRepository.AddAsync(newUser);
        //create token

        //return token
        var token = this._jwtTokenGenerator.GenerateToken(newUser.Id.Value, newUser.FirstName, newUser.LastName, newUser.Subscription.Name);

        return new AuthenticationResult(
            user,
            token);

    }
}