using ExpenseTracker.Core.UserAggregate;

namespace ExpenseTracker.Application.Common.Interfaces.Persistence;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetUserByEmailAsync(string email);
}