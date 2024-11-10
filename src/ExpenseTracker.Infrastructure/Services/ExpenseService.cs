

using ErrorOr;

using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.ExpenseOperations.Commands;
using ExpenseTracker.Application.ExpenseOperations.Queries;
using ExpenseTracker.Core.Entities;

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
            query.Description,
            query.CategoryId,
            userId);
        return await _expenseRepository.AddAsync(expense);
    }

    public async Task<GetExpenseQueryResult?> GetExpenseByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<(IEnumerable<GetExpenseQueryResult> Items, int TotalCount)> GetExpensesAsync(Guid userId, int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    // public async Task<(IEnumerable<GetExpenseQueryResult> Items, int TotalCount)> GetExpensesAsync(Guid userId, int page, int pageSize)
    // {
    //     var (items, totalCount) = await _expenseRepository.GetExpenseAsync(userId, page, pageSize);

    //     return (items, totalCount);
    // }

    public Task<UpdateExpenseResult> UpdateExpenseAsync(UpdateExpenseCommand query)
    {
        throw new NotImplementedException();
    }
}