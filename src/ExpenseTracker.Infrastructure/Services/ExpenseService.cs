using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.ExpenseOperations.Queries;
using ExpenseTracker.Application.Common.Errors;
using ExpenseTracker.Core.Entities;
using ErrorOr;
using ExpenseTracker.Application.ExpenseOperations.Commands.Common;
using ExpenseTracker.Application.ExpenseOperations.Commands.Create;
using ExpenseTracker.Application.ExpenseOperations.Commands.Update;

namespace ExpenseTracker.Infrastructure.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ICacheService _redisCacheService;
    private string GetExpensesCacheKey(Guid userId) => $"GetExpensesAsync_{userId}";
    private string GetExpenseByIdCacheKey(Guid id) => $"GetExpenseByIdAsync_{id}";
    public ExpenseService(
        IExpenseRepository expenseRepository,
        ICacheService redisCacheService)
    {
        _expenseRepository = expenseRepository;
        _redisCacheService = redisCacheService;
    }
    public async Task<ErrorOr<Guid>> AddExpenseAsync(CreateExpenseCommand command, Guid userId)
    {
        Expense expense = Expense.Create(
            command.Amount,
            command.Description,
            command.CategoryId,
            userId);

        var result = await _expenseRepository.AddAsync(expense);
        if (result <= 0)
        {
            return Errors.Expense.ExpenseCreationFailed;
        }
        else
        {
            await _redisCacheService.RemoveAsync(GetExpensesCacheKey(expense.UserId));
            return expense.Id;
        }
    }

    public async Task<ErrorOr<bool>> DeleteExpenseAsync(Guid id)
    {
        Expense expense = await _expenseRepository.GetByIdAsync(id);
        if (expense == null)
        {
            return Errors.Expense.ExpenseNotFound;
        }
        var result = await _expenseRepository.DeleteAsync(id);
        if (result <= 0)
        {
            return Errors.Expense.ExpenseDeletionFailed;
        }
        else
        {
            await _redisCacheService.RemoveAsync(GetExpenseByIdCacheKey(expense.Id));
            await _redisCacheService.RemoveAsync(GetExpensesCacheKey(expense.UserId));
            return true;
        }
    }

    public async Task<ErrorOr<ExpenseResult>> GetExpenseByIdAsync(Guid id)
    {
        string cacheKey = GetExpenseByIdCacheKey(id);
        var cachedData = await _redisCacheService.GetAsync<ExpenseResult>(cacheKey);
        if (cachedData != null)
        {
            return cachedData;
        }
        var expense = await _expenseRepository.GetExpenseByIdAsync(id);
        if (expense == null)
        {
            return Errors.Expense.ExpenseNotFound;
        }
        await _redisCacheService.SetAsync(cacheKey, expense);
        return expense;
    }

    public async Task<(IEnumerable<ExpenseResult> Items, int TotalCount)> GetExpensesAsync(Guid userId, int page, int pageSize)
    {
        var cacheKey = GetExpensesCacheKey(userId);
        var cachedData = await _redisCacheService.GetAsync<IEnumerable<ExpenseResult>>(cacheKey);
        if (cachedData != null && cachedData.Any())
        {
            return (cachedData, cachedData.Count());
        }
        var expenses = await _expenseRepository.GetExpensesByUserIdAsync(userId, page, pageSize);
        await _redisCacheService.SetAsync(cacheKey, expenses);
        return (expenses, expenses.Count());
    }

    public async Task<ErrorOr<ExpenseResult>> UpdateExpenseAsync(UpdateExpenseCommand command)
    {
        Expense expense = null;
        string cacheKey = GetExpenseByIdCacheKey(command.Id);
        var cachedData = await _redisCacheService.GetAsync<Expense>(cacheKey);
        if (cachedData != null)
        {
            expense = cachedData;
        }
        else
        {
            expense = await _expenseRepository.GetByIdAsync(command.Id);
        }
        if (expense == null)
        {
            return Errors.Expense.ExpenseNotFound;
        }
        expense.Update(command.Amount, command.Description, command.CategoryId);
        if (await _expenseRepository.UpdateAsync(expense) > 0)
        {
            await _redisCacheService.RemoveAsync(GetExpenseByIdCacheKey(expense.Id));
            await _redisCacheService.RemoveAsync(GetExpensesCacheKey(expense.UserId));
            return await GetExpenseByIdAsync(expense.Id);
        }
        else
        {
            return Errors.Expense.ExpenseUpdateFailed;
        }
    }

    public async Task<bool> CheckIfUserOwnsExpense(Guid userId, Guid expenseId)
    {
        Guid expenseUserId = await _expenseRepository.GetExpenseUserIdAsync(expenseId);
        if (expenseUserId == Guid.Empty || expenseUserId != userId)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}