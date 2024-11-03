using ExpenseTracker.Application.Authentication.Commands.Register;
using ExpenseTracker.Application.Authentication.Queries.Login;
using ExpenseTracker.Contracts.Authentication;

using Mapster;
namespace ExpenseTracker.API.Common.Mapping;
public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();
        config.NewConfig<string, AuthenticationResponse>()
        .Map(dest => dest.Token, src => src);

    }
}