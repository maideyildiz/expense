using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.ExpenseOperations.Queries;
using ExpenseTracker.Application.Common.Errors;
using ExpenseTracker.Core.Entities;
using ErrorOr;
using ExpenseTracker.Application.ExpenseOperations.Commands.Common;
using ExpenseTracker.Application.ExpenseOperations.Commands.Create;
using ExpenseTracker.Application.ExpenseOperations.Commands.Update;
using ExpenseTracker.Application.Common.Interfaces.Cache;

namespace ExpenseTracker.Infrastructure.Services;

public class ExpenseService : IExpenseService
{
    private readonly IDbRepository _dbRepository;
    private readonly ICacheService _redisCacheService;
    private string GetExpensesCacheKey(Guid userId) => $"GetExpensesAsync_{userId}";
    private string GetExpenseByIdCacheKey(Guid id) => $"GetExpenseByIdAsync_{id}";
    public ExpenseService(
        IDbRepository dbRepository,
        ICacheService redisCacheService)
    {
        _dbRepository = dbRepository;
        _redisCacheService = redisCacheService;
    }
    public async Task<ErrorOr<Guid>> AddExpenseAsync(CreateExpenseCommand command, Guid userId)
    {
        Expense expense = Expense.Create(
            command.Amount,
            command.Description,
            command.CategoryId,
            userId);
        var addSql = @"
            INSERT INTO Expenses (Amount, Description, CategoryId, UserId)
            VALUES (@Amount, @Description, @CategoryId, @UserId)
            RETURNING Id";
        var result = await _dbRepository.ExecuteAsync(addSql, expense);
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
        string sql = "SELECT * FROM Expenses WHERE Id = @Id";
        Expense expense = await _dbRepository.QueryFirstOrDefaultAsync<Expense>(sql, new { Id = id });
        if (expense == null)
        {
            return Errors.Expense.ExpenseNotFound;
        }
        string deleteSql = "DELETE FROM Expenses WHERE Id = @Id";
        var result = await _dbRepository.ExecuteAsync(deleteSql, new { id = id });
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
        string sql = @"
            SELECT e.Id, e.Amount, e.Description, e.UpdatedAt, c.Name AS CategoryName, e.UserId
            FROM Expenses e
            LEFT JOIN ExpenseCategories c ON e.CategoryId = c.Id
            WHERE e.Id = @Id";
        var expense = await _dbRepository.QueryFirstOrDefaultAsync<ExpenseResult>(sql, new { Id = id });
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
        var query = @"
            SELECT e.Id, e.Amount, e.Description, e.UpdatedAt, c.Name AS CategoryName, e.UserId
            FROM Expenses e
            LEFT JOIN ExpenseCategories c ON e.CategoryId = c.Id
            WHERE e.Id = @Id
            LIMIT @PageSize OFFSET @Offset";
        var expenses = await _dbRepository.QueryAsync<ExpenseResult>(query, new { UserId = userId, PageSize = pageSize, Offset = (page - 1) * pageSize });

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
            string sql = "SELECT * FROM Expenses WHERE Id = @Id";
            expense = await _dbRepository.QueryFirstOrDefaultAsync<Expense>(sql, new { Id = command.Id });
        }
        if (expense == null)
        {
            return Errors.Expense.ExpenseNotFound;
        }
        expense.Update(command.Amount, command.Description, command.CategoryId);
        string updateSql = @"
            UPDATE Expenses
            SET Amount = @Amount, Description = @Description, CategoryId = @CategoryId
            WHERE Id = @Id";
        if (await _dbRepository.ExecuteAsync(updateSql, expense) > 0)
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
        string sql = "SELECT Count(*) FROM Expenses WHERE Id = @Id AND UserId = @UserId";
        int count = await _dbRepository.ExecuteScalarAsync<int>(sql, new { Id = expenseId, UserId = userId });
        if (count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}