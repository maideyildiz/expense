using ExpenseTracker.Application.Common.Interfaces.Persistence;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Infrastructure.Abstractions;

namespace ExpenseTracker.Infrastructure.Persistence;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly IDatabaseConnection _dbConnection;

    public UserRepository(IDatabaseConnection dbConnection) : base(dbConnection, "User")
    {
        _dbConnection = dbConnection;
    }

    public async Task AddUserAsync(User user)
    {
        //hash password
        await AddAsync(user);
        throw new NotImplementedException();
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