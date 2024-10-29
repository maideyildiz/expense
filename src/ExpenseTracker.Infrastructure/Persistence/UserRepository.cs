namespace ExpenseTracker.Infrastructure.Persistence;
using ExpenseTracker.Application.Common.Interfaces.Persistence;
using ExpenseTracker.Core.UserAggregate;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private new readonly IDbRepository _dbRepository;

    public UserRepository(IDbRepository dbRepository)
        : base(dbRepository)
    {
        this._dbRepository = dbRepository;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email));

        var user = await this._dbRepository.QueryFirstOrDefaultAsync<User>(
            "SELECT * FROM Users WHERE Email = @Email",
            new { Email = email });

        return user ?? null;
    }
}