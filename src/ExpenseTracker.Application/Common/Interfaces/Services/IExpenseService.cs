using ErrorOr;

using ExpenseTracker.Application.ExpenseOperations.Commands;
using ExpenseTracker.Application.ExpenseOperations.Queries;
using ExpenseTracker.Core.ExpenseAggregate;

namespace ExpenseTracker.Application.Common.Interfaces.Services;

public interface IExpenseService
{
    Task<ErrorOr<int>> AddExpenseAsync(CreateExpenseCommand query, Guid userId);
    Task<(IEnumerable<GetExpensesQueryResult> Items, int TotalCount)> GetExpenseAsync(Guid userId, int page, int pageSize);
}