namespace ExpenseTracker.API.Common.Mapping;

using ExpenseTracker.Application.Authentication.Common;
using ExpenseTracker.Contracts.Authentication;

using Mapster;
public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
        .Map(dest => dest.Token, src => src.Token)
        .Map(dest => dest, src => src.User);
    }
}