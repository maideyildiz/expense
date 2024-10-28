using ExpenseTracker.Application.Common.Interfaces.Persistence;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Infrastructure.Persistence;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    protected new readonly IDbRepository _dbRepository;

    public UserRepository(IDbRepository dbRepository, string tableName) : base(dbRepository, tableName)
    {
        _dbRepository = dbRepository;
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

        var user = await _dbRepository.QueryFirstOrDefaultAsync<User>(
            "SELECT * FROM Users WHERE Email = @Email",
            new { Email = email });

        if (user == null)
            user = null;

        return user;
    }
}