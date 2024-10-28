using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);