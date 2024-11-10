namespace ExpenseTracker.Application.Common.Interfaces.Authentication;
using System.Security.Claims;

using ExpenseTracker.Application.Common.Interfaces.Persistence;
using ExpenseTracker.Core.Entities;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
    ClaimsPrincipal? ValidateToken(string token);
    string RevokeToken(string token);
}