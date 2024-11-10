using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.ExpenseOperations.Commands;
using ExpenseTracker.Application.ExpenseOperations.Queries;
using ExpenseTracker.Application.Common.Errors;
using ExpenseTracker.Core.Entities;
using ErrorOr;
using ExpenseTracker.Application.ExpenseOperations.Commands.Common;

namespace ExpenseTracker.Infrastructure.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    public ExpenseService(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }
    public async Task<ErrorOr<int>> AddExpenseAsync(CreateExpenseCommand command, Guid userId)
    {
        Expense expense = Expense.Create(
            command.Amount,
            command.Description,
            command.CategoryId,
            userId);
        return await _expenseRepository.AddAsync(expense);
    }

    public async Task<int> DeleteExpenseAsync(Guid id)
    {
        return await _expenseRepository.DeleteAsync(id);
    }

    public async Task<ErrorOr<ExpenseResult?>> GetExpenseByIdAsync(Guid id)
    {
        var expense = await _expenseRepository.GetExpenseByIdAsync(id);
        if (expense == null)
        {
            return Errors.Expense.ExpenseNotFound;
        }
        return expense;
    }

    public async Task<(IEnumerable<ExpenseResult> Items, int TotalCount)> GetExpensesAsync(Guid userId, int page, int pageSize)
    {
        return await _expenseRepository.GetExpensesByUserIdAsync(userId, page, pageSize);
    }

    public async Task<ErrorOr<ExpenseResult>> UpdateExpenseAsync(UpdateExpenseCommand command)
    {
        Expense? expense = await _expenseRepository.GetByIdAsync(command.Id);
        if (expense == null)
        {
            return Errors.Expense.ExpenseNotFound;
        }
        expense.Update(command.Amount, command.Description, command.CategoryId);
        if (await _expenseRepository.UpdateAsync(expense) > 0)
        {
            return await GetExpenseByIdAsync(expense.Id);
        }
        else
        {
            return Errors.Expense.ExpenseUpdateFailed;
        }
    }
}