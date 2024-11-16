using ExpenseTracker.Application.ExpenseOperations.Commands.Common;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;

public interface IExpenseRepository : IBaseRepository<Expense>
{
    Task<ExpenseResult> GetExpenseByIdAsync(Guid id);
    Task<IEnumerable<ExpenseResult>> GetExpensesByUserIdAsync(Guid userId, int page, int pageSize);
    Task<Guid> GetExpenseUserIdAsync(Guid expenseId);
}