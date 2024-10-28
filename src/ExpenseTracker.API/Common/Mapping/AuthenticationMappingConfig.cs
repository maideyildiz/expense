namespace ExpenseTracker.API.Common.Mapping;

using ExpenseTracker.Application.Authentication.Commands.Register;
using ExpenseTracker.Application.Authentication.Common;
using ExpenseTracker.Application.Authentication.Queries.Login;
using ExpenseTracker.Contracts.Authentication;

using Mapster;
public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
        .Map(dest => dest, src => src.User);
    }
}