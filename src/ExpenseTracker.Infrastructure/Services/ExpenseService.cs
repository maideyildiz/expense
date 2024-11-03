
using ErrorOr;

using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.ExpenseOperations.Commands;
using ExpenseTracker.Core.ExpenseAggregate;

namespace ExpenseTracker.Infrastructure.Services;

public class ExpenseService : IExpenseService
{
    private readonly IBaseRepository<Expense> _expenseRepository;
    public ExpenseService(IBaseRepository<Expense> expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }
    public async Task<ErrorOr<int>> AddExpenseAsync(CreateExpenseCommand query, Guid userId)
    {
        Expense expense = Expense.Create(
            query.Amount,
            DateTime.UtcNow,
            DateTime.UtcNow,
            query.Description,
            query.CategoryId,
            userId);
        return await _expenseRepository.AddAsync(expense);
    }
}