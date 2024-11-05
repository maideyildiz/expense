using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.ExpenseOperations.Queries;
using ExpenseTracker.Core.ExpenseAggregate;

namespace ExpenseTracker.Infrastructure.Persistence.Repositories;

public class ExpenseRepository : BaseRepository<Expense>, IExpenseRepository
{
    private new readonly IDbRepository _dbRepository;
    public ExpenseRepository(IDbRepository dbRepository)
    : base(dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public async Task<(IEnumerable<GetExpensesQueryResult> Items, int TotalCount)> GetExpenseAsync(Guid userId, int page, int pageSize)
    {
        int offset = (page - 1) * pageSize;

        string query = @"
        SELECT e.Id, e.Amount, e.CreatedAt, e.Description, c.Name AS CategoryName
        FROM Expenses e
        JOIN Categories c ON e.CategoryId = c.Id
        WHERE e.UserId = @UserId
        LIMIT @PageSize OFFSET @Offset;"; // Use LIMIT and OFFSET

        var items = await _dbRepository.QueryAsync<GetExpensesQueryResult>(query, new { UserId = userId, PageSize = pageSize, Offset = offset });

        string countQuery = "SELECT COUNT(*) FROM Expenses WHERE UserId = @UserId;";
        int totalCount = await _dbRepository.ExecuteScalarAsync<int>(countQuery, new { UserId = userId });

        return (items, totalCount);
    }


}