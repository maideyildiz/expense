using ExpenseTracker.Core.Models;
using ExpenseTracker.Core.Repositories;
using MongoDB.Driver;
using ExpenseTracker.Infrastructure.Data.DbSettings;
using ExpenseTracker.Infrastructure.Helpers;

namespace ExpenseTracker.Infrastructure.Services;
public class UserService : BaseService<User>, IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserService(DbOptions dbOptions) : base(dbOptions, "User")
    {
        _users = _collection;
    }
    public async Task RegisterAsync(User user)
    {
        var existingUser = await _users.Find(u => u.Username == user.Username).FirstOrDefaultAsync();
        if (existingUser != null)
        {
            throw new InvalidOperationException("Username already exists.");
        }

        user.Password = PasswordHasher.HashPassword(user.Password);

        await _users.InsertOneAsync(user);
    }

    public async Task<User?> LoginAsync(string username, string password)
    {
        var user = await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
        if (user == null)
        {
            return null;
        }

        var hashedInputPassword = PasswordHasher.HashPassword(password);
        if (PasswordHasher.VerifyPassword(hashedInputPassword, user.Password))
        {
            return null;
        }

        return user;
    }

}

