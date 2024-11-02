using ExpenseTracker.Application.Common.Interfaces.Persistence;
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Core.UserAggregate;
using ExpenseTracker.Infrastructure.Helpers;

namespace ExpenseTracker.Infrastructure.Persistence.Repositories;
public class UserRepository : BaseRepository<User>, IUserRepository
{
    private new readonly IDbRepository _dbRepository;

    public UserRepository(IDbRepository dbRepository)
        : base(dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email));

        var user = await _dbRepository.QueryFirstOrDefaultAsync<User>(
            "SELECT * FROM Users WHERE Email = @Email",
            new { Email = email });

        return user ?? null;
    }


    public async Task AddUserAsync(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        await _dbRepository.ExecuteAsync(
            "INSERT INTO Users (Id, FirstName, LastName, Email, PasswordHash, MonthlySalary, YearlySalary, CreatedAt, UpdatedAt, LastLoginAt, IsActive, SubscriptionId, CityId) " +
            "VALUES (@Id, @FirstName, @LastName, @Email, @PasswordHash, @MonthlySalary, @YearlySalary, @CreatedAt, @UpdatedAt, @LastLoginAt, @IsActive, @SubscriptionId, @CityId)",
            new
            {
                Id = user.Id, // Assuming UserId has a Value property
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                MonthlySalary = user.MonthlySalary,
                YearlySalary = user.YearlySalary,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                LastLoginAt = user.LastLoginAt,
                IsActive = user.IsActive,
                SubscriptionId = user.SubscriptionId, // Assuming SubscriptionId has a Value property
                CityId = user.CityId, // Assuming CityId has a Value property
            });
    }
}