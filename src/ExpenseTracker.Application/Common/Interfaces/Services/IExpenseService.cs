using ErrorOr;

using ExpenseTracker.Application.ExpenseOperations.Commands;
using ExpenseTracker.Application.ExpenseOperations.Queries;
using ExpenseTracker.Core.ExpenseAggregate;

namespace ExpenseTracker.Application.Common.Interfaces.Services;

public interface IExpenseService
{
    public Task<ErrorOr<int>> AddExpenseAsync(CreateExpenseCommand query, Guid userId);
    public Task<List<GetExpensesQueryResult>> GetExpenseAsync(Guid userId);
}