using ExpenseTracker.Application.CategoryOperations.Common;
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Infrastructure.Persistence.Repositories;

public class InvestmentCategoryRepository : BaseRepository<InvestmentCategory>, IInvestmentCategoryRepository
{
    private readonly IDbRepository _dbRepository;
    public InvestmentCategoryRepository(IDbRepository dbRepository)
    : base(dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public async Task<(IEnumerable<CategoryResult> Items, int TotalCount)> GetInvestmentCategoriesAsync(int page, int pageSize)
    {
        string query = @"
        SELECT ic.Id, ic.Name
        FROM InvestmentCategories ic
        LIMIT @PageSize OFFSET @Offset";

        var countries = await _dbRepository.QueryAsync<CategoryResult>(query, new { PageSize = pageSize, Offset = (page - 1) * pageSize });

        string countQuery = "SELECT COUNT(*) FROM InvestmentCategories";
        int totalCount = await _dbRepository.ExecuteScalarAsync<int>(countQuery);

        return (countries, totalCount);
    }

    public async Task<CategoryResult?> GetInvestmentCategoryByIdAsync(Guid id)
    {
        var query = @"
            SELECT ic.Id, ic.Name
            FROM InvestmentCategories ic
            WHERE ic.Id = @Id";

        return await _dbRepository.QuerySingleOrDefaultAsync<CategoryResult>(query, new { Id = id });
    }
}