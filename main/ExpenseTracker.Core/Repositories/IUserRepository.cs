using ExpenseTracker.Core.Models;
namespace ExpenseTracker.Core.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task RegisterAsync(User user);
    Task<User?> LoginAsync(string email, string password);
}