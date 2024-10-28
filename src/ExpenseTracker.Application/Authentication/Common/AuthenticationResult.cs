

using ExpenseTracker.Core.UserAggregate;

namespace ExpenseTracker.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);