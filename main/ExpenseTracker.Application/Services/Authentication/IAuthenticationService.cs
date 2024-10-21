namespace ExpenseTracker.Application.Services.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticationResult> RegisterAsync(string name, string surname, string email, string password);
    Task<AuthenticationResult> LoginAsync(string email, string password);
}
