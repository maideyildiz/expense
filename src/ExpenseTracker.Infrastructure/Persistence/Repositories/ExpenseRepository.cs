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

    public async Task<IEnumerable<GetExpensesQueryResult>> GetExpenseAsync(Guid userId)
    {
        string query = @"
        SELECT e.Id, e.Amount, e.CreatedAt, e.Description, c.Name AS CategoryName
        FROM Expenses e
        JOIN Categories c ON e.CategoryId = c.Id
        WHERE e.UserId = @UserId;";

        return await _dbRepository.QueryAsync<GetExpensesQueryResult>(query, new { UserId = userId });
    }
}