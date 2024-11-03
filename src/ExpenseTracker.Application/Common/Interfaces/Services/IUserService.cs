using ErrorOr;
using ExpenseTracker.Core.UserAggregate;
using ExpenseTracker.Application.Authentication.Commands.Register;
using ExpenseTracker.Application.Authentication.Queries.Login;

namespace ExpenseTracker.Application.Common.Interfaces.Services;

public interface IUserService
{
    public Task<ErrorOr<string>> RegisterUserAsync(RegisterCommand command);
    public Task<ErrorOr<string>> LoginUserAsync(LoginQuery query);
}