using ExpenseTracker.Application.UserOperations.Commands.Update;
using ExpenseTracker.Application.UserOperations.Common;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<UserResult> GetUserDetailsAsync(Guid userId);
}