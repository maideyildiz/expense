
using ErrorOr;

using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.ExpenseOperations.Commands;
using ExpenseTracker.Application.ExpenseOperations.Queries;
using ExpenseTracker.Core.ExpenseAggregate;

namespace ExpenseTracker.Infrastructure.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    public ExpenseService(IExpenseRepository expenseRepository)
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

    public async Task<GetExpenseQueryResult?> GetExpenseByIdAsync(Guid id)
    {
        return await _expenseRepository.GetExpenseByIdAsync(id);
    }

    public async Task<(IEnumerable<GetExpenseQueryResult> Items, int TotalCount)> GetExpensesAsync(Guid userId, int page, int pageSize)
    {
        var (items, totalCount) = await _expenseRepository.GetExpenseAsync(userId, page, pageSize);

        return (items, totalCount);
    }

}