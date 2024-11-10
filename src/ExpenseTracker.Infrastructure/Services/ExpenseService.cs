using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.ExpenseOperations.Commands;
using ExpenseTracker.Application.ExpenseOperations.Queries;
using ExpenseTracker.Application.Common.Errors;
using ExpenseTracker.Core.Entities;
using ErrorOr;

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

    public async Task<ErrorOr<GetExpenseQueryResult?>> GetExpenseByIdAsync(Guid id)
    {
        Expense? expense = await _expenseRepository.GetByIdAsync(id);
        if (expense == null)
        {
            return Errors.Expense.ExpenseNotFound;
        }
        GetExpenseQueryResult getExpenseQueryResult = new GetExpenseQueryResult(expense.Id, expense.Amount, expense.CreatedAt, expense.Description, "");
        return getExpenseQueryResult;
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

    public async Task<ErrorOr<UpdateExpenseResult>> UpdateExpenseAsync(UpdateExpenseCommand query)
    {
        Expense? expense = await _expenseRepository.GetByIdAsync(query.Id);
        if (expense == null)
        {
            return Errors.Expense.ExpenseNotFound;
        }
        if (await _expenseRepository.UpdateAsync(expense) > 0)
        {
            UpdateExpenseResult updateExpenseResult = new UpdateExpenseResult(expense.Amount, expense.Description, expense.UpdatedAt, query.Description);
            return updateExpenseResult;
        }
        else
        {
            return Errors.Expense.ExpenseUpdateFailed;
        }


    }
}