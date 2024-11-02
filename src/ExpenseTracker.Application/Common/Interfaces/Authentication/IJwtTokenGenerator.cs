namespace ExpenseTracker.Application.Common.Interfaces.Authentication;
using System.Security.Claims;
using ExpenseTracker.Core.UserAggregate;
using ExpenseTracker.Application.Common.Interfaces.Persistence;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
    ClaimsPrincipal? ValidateToken(string token);
    string RevokeToken(string token);
}