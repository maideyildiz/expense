using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Application.Common.Interfaces.Persistence;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetUserByEmailAsync(string email);
    Task AddUserAsync(User user);
}