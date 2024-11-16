
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.InvestmentOperations.Common;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Infrastructure.Persistence.Repositories;

public class InvestmentRepository : BaseRepository<Investment>, IInvestmentRepository
{
    private readonly IDbRepository _dbRepository;
    public InvestmentRepository(IDbRepository dbRepository)
    : base(dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public async Task<InvestmentResult> GetInvestmentByIdAsync(Guid id)
    {
        var query = @"
            SELECT i.Id, i.Amount, i.Description, i.UpdatedAt, c.Name AS CategoryName, i.UserId
            FROM Investments i
            LEFT JOIN InvestmentCategories c ON i.CategoryId = c.Id
            WHERE i.Id = @Id";

        var investment = await _dbRepository.QuerySingleOrDefaultAsync<InvestmentResult>(query, new { Id = id });

        return investment;
    }

    public async Task<IEnumerable<InvestmentResult>> GetInvestmentsByUserIdAsync(Guid userId, int page, int pageSize)
    {
        string query = @"
        SELECT i.Id, i.Amount, i.Description, i.UpdatedAt, c.Name AS CategoryName, i.UserId
        FROM Investments i
        LEFT JOIN InvestmentCategories c ON i.CategoryId = c.Id
        WHERE i.UserId = @UserId
        LIMIT @PageSize OFFSET @Offset";

        var investments = await _dbRepository.QueryAsync<InvestmentResult>(
            query,
            new { UserId = userId, PageSize = pageSize, Offset = (page - 1) * pageSize });

        string countQuery = "SELECT COUNT(*) FROM Investments WHERE UserId = @UserId";
        int totalCount = await _dbRepository.ExecuteScalarAsync<int>(countQuery, new { UserId = userId });

        return investments;
    }

    public async Task<Guid> GetInvestmentUserIdAsync(Guid investmentId)
    {
        var query = "SELECT UserId FROM Investments WHERE Id = @InvestmentId";
        return await _dbRepository.QuerySingleOrDefaultAsync<Guid>(query, new { InvestmentId = investmentId });
    }
}