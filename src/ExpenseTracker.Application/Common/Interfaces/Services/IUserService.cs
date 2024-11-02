using ErrorOr;
using ExpenseTracker.Application.Authentication.Common;
using ExpenseTracker.Core.UserAggregate;
using ExpenseTracker.Application.Authentication.Commands.Register;

namespace ExpenseTracker.Application.Common.Interfaces.Services;

public interface IUserService
{
    public Task<ErrorOr<AuthenticationResult>> RegisterUserAsync(RegisterCommand command);
}