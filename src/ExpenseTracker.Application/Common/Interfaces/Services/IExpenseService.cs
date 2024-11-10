using ErrorOr;

using ExpenseTracker.Application.ExpenseOperations.Commands;
using ExpenseTracker.Application.ExpenseOperations.Queries;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Application.Common.Interfaces.Services;

public interface IExpenseService
{
    Task<ErrorOr<int>> AddExpenseAsync(CreateExpenseCommand query, Guid userId);
    Task<(IEnumerable<GetExpenseQueryResult> Items, int TotalCount)> GetExpensesAsync(Guid userId, int page, int pageSize);
    Task<ErrorOr<GetExpenseQueryResult?>> GetExpenseByIdAsync(Guid userId);
    Task<ErrorOr<UpdateExpenseResult>> UpdateExpenseAsync(UpdateExpenseCommand query);
}