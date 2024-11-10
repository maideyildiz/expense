
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.ExpenseOperations.Commands;
using ExpenseTracker.Application.ExpenseOperations.Commands.Common;
using ExpenseTracker.Application.ExpenseOperations.Queries;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Infrastructure.Persistence.Repositories;

public class ExpenseRepository : BaseRepository<Expense>, IExpenseRepository
{
    private readonly IDbRepository _dbRepository;
    public ExpenseRepository(IDbRepository dbRepository)
    : base(dbRepository)
    {
        _dbRepository = dbRepository;
    }
    public async Task<ExpenseResult> GetExpenseByIdAsync(Guid id)
    {
        var query = @"
            SELECT e.Id, e.Amount, e.Description, e.UpdatedAt, c.Name AS CategoryName, e.UserId
            FROM Expenses e
            LEFT JOIN ExpenseCategories c ON e.CategoryId = c.Id
            WHERE e.Id = @Id";

        var expense = await _dbRepository.QuerySingleOrDefaultAsync<ExpenseResult>(query, new { Id = id });

        return expense;
    }

    public async Task<(IEnumerable<ExpenseResult> Items, int TotalCount)> GetExpensesByUserIdAsync(Guid userId, int page, int pageSize)
    {
        string query = @"
        SELECT e.Id, e.Amount, e.Description, e.UpdatedAt, c.Name AS CategoryName, e.UserId
        FROM Expenses e
        LEFT JOIN ExpenseCategories c ON e.CategoryId = c.Id
        WHERE e.UserId = @UserId
        LIMIT @PageSize OFFSET @Offset";

        var expenses = await _dbRepository.QueryAsync<ExpenseResult>(
            query,
            new { UserId = userId, PageSize = pageSize, Offset = (page - 1) * pageSize });

        string countQuery = "SELECT COUNT(*) FROM Expenses WHERE UserId = @UserId";
        int totalCount = await _dbRepository.ExecuteScalarAsync<int>(countQuery, new { UserId = userId });

        return (expenses, totalCount);
    }

    public async Task<Guid> GetExpenseUserIdAsync(Guid expenseId)
    {
        var query = "SELECT UserId FROM Expenses WHERE Id = @ExpenseId";
        return await _dbRepository.QuerySingleOrDefaultAsync<Guid>(query, new { ExpenseId = expenseId });
    }
}