using ErrorOr;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Application.Authentication.Commands.Register;
using ExpenseTracker.Application.Authentication.Queries.Login;

namespace ExpenseTracker.Application.Common.Interfaces.Services;

public interface IUserService
{
    ErrorOr<Guid> GetUserId();
    public Task<ErrorOr<string>> RegisterUserAsync(RegisterCommand command);
    public Task<ErrorOr<string>> LoginUserAsync(LoginQuery query);
}