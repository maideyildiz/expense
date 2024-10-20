using System.Security.Claims;
using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Infrastructure.Abstractions.Auth;

public interface ITokenService
{
    string GenerateToken(User user);
    ClaimsPrincipal? ValidateToken(string token);
    Task<string> RefreshToken(string expiredToken);
    void RevokeToken(string token);
}