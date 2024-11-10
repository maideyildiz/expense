
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.ExpenseOperations.Commands;
using ExpenseTracker.Application.ExpenseOperations.Commands.Common;
using ExpenseTracker.Application.ExpenseOperations.Queries;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Infrastructure.Persistence.Repositories;

public class ExpenseRepository : BaseRepository<Expense>, IExpenseRepository
{
    private readonly IDbRepository _dbRepository;
    public ExpenseRepository(IDbRepository dbRepository) : base(dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public async Task<(IEnumerable<ExpenseResult> Items, int TotalCount)> GetExpensesByUserIdAsync(Guid userId, int page, int pageSize)
    {
        string query = @"
        SELECT e.Id, e.Amount, e.CreatedAt, e.Description, c.CategoryName
        FROM Expenses e
        LEFT JOIN Categories c ON e.CategoryId = c.Id
        WHERE e.UserId = @UserId
        LIMIT @PageSize OFFSET @Offset";

        var expenses = await _dbRepository.QueryAsync<ExpenseResult>(
            query,
            new { UserId = userId, PageSize = pageSize, Offset = (page - 1) * pageSize });

        string countQuery = "SELECT COUNT(*) FROM Expenses WHERE UserId = @UserId";
        int totalCount = await _dbRepository.ExecuteScalarAsync<int>(countQuery, new { UserId = userId });

        return (expenses, totalCount);
    }

    public async Task<ExpenseResult> UpdateExpensesByUserIdAsync(Expense expense)
    {
        string query = @"
        UPDATE Expenses
        SET Amount = @Amount, Description = @Description, CategoryId = @CategoryId, UpdatedAt = NOW()
        WHERE Id = @Id AND UserId = @UserId";

        int rowsAffected = await _dbRepository.ExecuteAsync(query, new
        {
            Id = expense.Id,
            UserId = expense.UserId,
            Amount = expense.Amount,
            Description = expense.Description,
            CategoryId = expense.CategoryId,
        });

        if (rowsAffected == 0)
        {
            return null;
        }

        return null;
    }
}