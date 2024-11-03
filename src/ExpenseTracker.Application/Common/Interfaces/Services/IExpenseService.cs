using ErrorOr;

using ExpenseTracker.Application.ExpenseOperations.Commands;

namespace ExpenseTracker.Application.Common.Interfaces.Services;

public interface IExpenseService
{
    public Task<ErrorOr<int>> AddExpenseAsync(CreateExpenseCommand query, Guid userId);
}