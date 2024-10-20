using ExpenseTracker.Core.Models;
using ExpenseTracker.Infrastructure.Abstractions;
using ExpenseTracker.Infrastructure.Helpers;

namespace ExpenseTracker.Infrastructure.Services;

public class UserService : BaseService<User>, IUserService
{
    private readonly IDatabaseConnection _dbConnection;

    public UserService(IDatabaseConnection dbConnection) : base(dbConnection, "User")
    {
        this._dbConnection = dbConnection;
    }

    public async Task<User?> RegisterAsync(User user)
    {
        if (user is null)
            throw new ArgumentNullException(nameof(user), $"{nameof(user)} is null.");

        if (user.Email == null || string.IsNullOrWhiteSpace(user.Email))
            throw new ArgumentException($"Email address is invalid.", nameof(user.Email));

        if (string.IsNullOrWhiteSpace(user.Password))
            throw new ArgumentException($"Password is invalid.", nameof(user.Password));


        var existingUser = await GetUserByEmailAsync(user.Email);

        if (existingUser != null)
        {
            throw new InvalidOperationException("Email already exists.");
        }
        try
        {
            user.Password = PasswordHasher.HashPassword(user.Password);

            var query = @"INSERT INTO Users (Id, Email, Password) VALUES (@Id, @Email, @Password)";
            await _dbConnection.ExecuteAsync(query, new
            {
                Id = Guid.NewGuid(),
                user.Email,
                user.Name,
                user.Surname,
                user.Password
            });

            return user;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Error: {ex.Message}");
            return null;
        }
    }

    public async Task<User> LoginAsync(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email));

        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException(nameof(password));

        var user = await GetUserByEmailAsync(email);

        if (user == null)
            throw new UnauthorizedAccessException("Invalid email or password.");

        if (!PasswordHasher.VerifyPassword(password, user.Password))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        return user;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email));

        var user = await _dbConnection.QueryFirstOrDefaultAsync<User>(
            "SELECT * FROM Users WHERE Email = @Email",
            new { Email = email });

        if (user == null)
            user = null;

        return user;
    }
}

