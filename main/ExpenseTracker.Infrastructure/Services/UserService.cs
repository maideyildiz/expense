using ExpenseTracker.Core.Models;
using MongoDB.Driver;
using ExpenseTracker.Infrastructure.Data.DbSettings;
using ExpenseTracker.Infrastructure.Helpers;
using ExpenseTracker.Core.Repositories;

namespace ExpenseTracker.Infrastructure.Services;
public class UserService : BaseService<User>, IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserService(IMongoDbContext context) : base(context, "User")
    {
        _users = context.GetCollection<User>("User");
    }
    public async Task RegisterAsync(User user)
    {
        if (user is null)
            throw new ArgumentNullException(nameof(user), $"{nameof(user)} is null.");


        if (string.IsNullOrWhiteSpace(user.Email))
            throw new ArgumentException($"Email address is invalid.", nameof(user.Email));


        if (string.IsNullOrWhiteSpace(user.Password))
            throw new ArgumentException($"Password is invalid.", nameof(user.Password));


        var existingUser = await _users.Find(u => u.Email == user.Email).FirstOrDefaultAsync();
        if (existingUser != null)
        {
            throw new InvalidOperationException("Email already exists.");
        }

        user.Password = PasswordHasher.HashPassword(user.Password);

        await _users.InsertOneAsync(user);
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email));

        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException(nameof(password));

        var user = await _users.Find(u => u.Email == email).FirstOrDefaultAsync();

        if (user == null)
            throw new ArgumentNullException(nameof(user)); // Burada daha açıklayıcı bir hata fırlatabilirsiniz

        var hashedInputPassword = PasswordHasher.HashPassword(password);
        if (PasswordHasher.VerifyPassword(hashedInputPassword, user.Password))
        {
            return null;
        }

        return user;
    }

}

