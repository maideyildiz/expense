namespace ExpenseTracker.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid id, string name, string surname);

    //     ClaimsPrincipal? ValidateToken(string token);
    // Task<string> RefreshToken(string expiredToken);
    // void RevokeToken(string token);
}