using ExpenseTracker.Core.ExpenseAggregate;
using ExpenseTracker.Application.ExpenseOperations.Queries;
using ExpenseTracker.Application.ExpenseOperations.Commands;
namespace ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;

public interface IExpenseRepository : IBaseRepository<Expense>
{
    Task<GetExpenseQueryResult?> GetExpenseByIdAsync(Guid id);
    Task<(IEnumerable<GetExpenseQueryResult> Items, int TotalCount)> GetExpenseAsync(Guid userId, int page, int pageSize);
    Task<Expense> UpdateExpenseAsync(Expense expense);
}