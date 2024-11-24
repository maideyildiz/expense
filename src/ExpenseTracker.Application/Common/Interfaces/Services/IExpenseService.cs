using ExpenseTracker.Application.ExpenseOperations.Commands.Common;
using ExpenseTracker.Application.ExpenseOperations.Commands.Create;
using ExpenseTracker.Application.ExpenseOperations.Commands.Update;
using ExpenseTracker.Core.Entities;
using ErrorOr;
namespace ExpenseTracker.Application.Common.Interfaces.Services;

public interface IExpenseService
{
    Task<ErrorOr<Guid>> AddExpenseAsync(CreateExpenseCommand command, Guid userId);
    Task<(IEnumerable<ExpenseResult> Items, int TotalCount)> GetExpensesAsync(Guid userId, int page, int pageSize);
    Task<ErrorOr<ExpenseResult>> GetExpenseByIdAsync(Guid id);
    Task<ErrorOr<ExpenseResult>> UpdateExpenseAsync(UpdateExpenseCommand command);
    Task<ErrorOr<bool>> DeleteExpenseAsync(Guid id);
    Task<bool> CheckIfUserOwnsExpense(Guid userId, Guid expenseId);
}