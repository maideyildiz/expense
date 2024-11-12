
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.UserOperations.Commands.Update;
using ExpenseTracker.Application.UserOperations.Common;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Infrastructure.Persistence.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly IDbRepository _dbRepository;
    public UserRepository(IDbRepository dbRepository)
    : base(dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public async Task<UserResult> GetUserDetailsAsync(Guid userId)
    {
        var query = @"
            SELECT u.Id, u.FirstName, u.LastName, u.Email, u.MonthlySalary, u.YearlySalary, c.Name AS CityName
            FROM Users u
            LEFT JOIN Cities c ON u.CityId = c.Id
            WHERE u.Id = @Id";

        var user = await _dbRepository.QuerySingleOrDefaultAsync<UserResult>(query, new { Id = userId });
        return user;
    }
}
