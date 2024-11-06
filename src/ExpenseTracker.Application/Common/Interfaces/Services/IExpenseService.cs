using ErrorOr;

using ExpenseTracker.Application.ExpenseOperations.Commands;
using ExpenseTracker.Application.ExpenseOperations.Queries;
using ExpenseTracker.Core.ExpenseAggregate;

namespace ExpenseTracker.Application.Common.Interfaces.Services;

public interface IExpenseService
{
    Task<ErrorOr<int>> AddExpenseAsync(CreateExpenseCommand query, Guid userId);
    Task<(IEnumerable<GetExpenseQueryResult> Items, int TotalCount)> GetExpensesAsync(Guid userId, int page, int pageSize);
    Task<GetExpenseQueryResult?> GetExpenseByIdAsync(Guid userId);
    Task<UpdateExpenseResult> UpdateExpenseAsync(UpdateExpenseCommand query);
}