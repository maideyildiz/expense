using ExpenseTracker.Core.ExpenseAggregate;
using ExpenseTracker.Application.ExpenseOperations.Queries;
namespace ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;

public interface IExpenseRepository : IBaseRepository<Expense>
{
    Task<(IEnumerable<GetExpensesQueryResult> Items, int TotalCount)> GetExpenseAsync(Guid userId, int page, int pageSize);
}