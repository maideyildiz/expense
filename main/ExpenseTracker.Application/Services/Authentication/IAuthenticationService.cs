using ErrorOr;

namespace ExpenseTracker.Application.Services.Authentication;

public interface IAuthenticationService
{
    Task<ErrorOr<AuthenticationResult>> RegisterAsync(string name, string surname, string email, string password);
    Task<ErrorOr<AuthenticationResult>> LoginAsync(string email, string password);
}
