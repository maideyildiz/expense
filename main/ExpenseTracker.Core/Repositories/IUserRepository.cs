using ExpenseTracker.Core.Models;
namespace ExpenseTracker.Core.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<bool> RegisterAsync(User user);
    Task<string> LoginAsync(string email, string password);
    Task<User> GetUserByEmailAsync(string email);
}