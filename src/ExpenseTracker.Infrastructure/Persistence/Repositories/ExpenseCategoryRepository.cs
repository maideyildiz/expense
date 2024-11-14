using ExpenseTracker.Application.CategoryOperations.Common;
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Infrastructure.Persistence.Repositories;

public class ExpenseCategoryRepository : BaseRepository<ExpenseCategory>, IExpenseCategoryRepository
{
    private readonly IDbRepository _dbRepository;
    public ExpenseCategoryRepository(IDbRepository dbRepository)
    : base(dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public async Task<(IEnumerable<CategoryResult> Items, int TotalCount)> GetExpenseCategoriesAsync(int page, int pageSize)
    {
        string query = @"
        SELECT ec.Id, ec.Name
        FROM ExpenseCategories ec
        LIMIT @PageSize OFFSET @Offset";

        var countries = await _dbRepository.QueryAsync<CategoryResult>(query, new { PageSize = pageSize, Offset = (page - 1) * pageSize });

        string countQuery = "SELECT COUNT(*) FROM ExpenseCategories";
        int totalCount = await _dbRepository.ExecuteScalarAsync<int>(countQuery);

        return (countries, totalCount);
    }

    public async Task<CategoryResult?> GetExpenseCategoryByIdAsync(Guid id)
    {
        var query = @"
            SELECT ec.Id, ec.Name
            FROM ExpenseCategories ec
            WHERE ec.Id = @Id";

        return await _dbRepository.QuerySingleOrDefaultAsync<CategoryResult>(query, new { Id = id });
    }
}