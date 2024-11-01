using ErrorOr;
using MediatR;
using ExpenseTracker.Application.Authentication.Common;
using ExpenseTracker.Application.Common.Interfaces.Authentication;
using ExpenseTracker.Application.Common.Interfaces.Persistence;
using ExpenseTracker.Core.Common.Errors;
using ExpenseTracker.Core.UserAggregate;
using ExpenseTracker.Core.UserAggregate.Entities;
using ExpenseTracker.Core.Common.Entities;
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
namespace ExpenseTracker.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IBaseRepository<Subscription> _subscriptionRepository;
    private readonly IBaseRepository<City> _cityRepository;

    public RegisterCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository,
        IBaseRepository<Subscription> subscriptionRepository,
        IBaseRepository<City> cityRepository)
    {
        this._jwtTokenGenerator = jwtTokenGenerator;
        this._userRepository = userRepository;
        _subscriptionRepository = subscriptionRepository;
        _cityRepository = cityRepository;
    }


    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var user = await this._userRepository.GetUserByEmailAsync(command.Email);
        if (user is not null)
            return Errors.Authentication.InvalidCredentials;

        var subscription = await _subscriptionRepository.GetByIdAsync(command.SubscriptionId);

        if (subscription is null)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var city = await _cityRepository.GetByIdAsync(command.CityId);
        if (city is null)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        //create user
        var newUser = User.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            command.Password,
            subscription.Id,
            city.Id);


        var result = await this._userRepository.AddAsync(newUser);

        if (result is 0)
            return Errors.Authentication.InvalidCredentials;

        //create token
        var token = this._jwtTokenGenerator.GenerateToken(newUser.Id.Value, newUser.FirstName, newUser.LastName, subscription.Name);
        return new AuthenticationResult(
            user!,
            token);
    }
}