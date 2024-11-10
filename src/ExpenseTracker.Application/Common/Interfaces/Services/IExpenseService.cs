using ErrorOr;

using ExpenseTracker.Application.ExpenseOperations.Commands;
using ExpenseTracker.Application.ExpenseOperations.Commands.Common;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Application.Common.Interfaces.Services;

public interface IExpenseService
{
    Task<ErrorOr<Guid>> AddExpenseAsync(CreateExpenseCommand query, Guid userId);
    Task<(IEnumerable<ExpenseResult> Items, int TotalCount)> GetExpensesAsync(Guid userId, int page, int pageSize);
    Task<ErrorOr<ExpenseResult>> GetExpenseByIdAsync(Guid id);
    Task<ErrorOr<ExpenseResult>> UpdateExpenseAsync(UpdateExpenseCommand query);
    Task<int> DeleteExpenseAsync(Guid id);
    Task<bool> CheckIfUserOwnsExpense(Guid userId, Guid expenseId);
}