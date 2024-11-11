using ErrorOr;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Application.Authentication.Commands.Register;
using ExpenseTracker.Application.Authentication.Queries.Login;
using ExpenseTracker.Application.UserOperations.Common;
using ExpenseTracker.Application.UserOperations.Commands.Update;

namespace ExpenseTracker.Application.Common.Interfaces.Services;

public interface IUserService
{
    ErrorOr<Guid> GetUserId();
    public Task<ErrorOr<string>> RegisterUserAsync(RegisterCommand command);
    public Task<ErrorOr<string>> LoginUserAsync(LoginQuery query);
    public Task<ErrorOr<UserResult>> GetUserDetailsAsync(Guid userId);
    public Task<ErrorOr<UserResult>> UpdateUserDetailsAsync(UpdateUserProfileCommand command, Guid userId);
}