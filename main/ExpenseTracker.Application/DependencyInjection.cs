using ExpenseTracker.Application.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        // services.AddScoped<IUserService, UserService>();
        // services.AddScoped<ITokenService, TokenService>();
        return services;
    }
}