using Microsoft.AspNetCore.Http;
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Core.ExpenseAggregate;
using ExpenseTracker.Core.UserAggregate;
using ExpenseTracker.Core.Common.Entities;
using System.Security.Claims;
using ExpenseTracker.Application.Common.Errors;
using ErrorOr;
using MediatR;

namespace ExpenseTracker.Application.ExpenseOperations.Commands;

public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, ErrorOr<int>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IBaseRepository<Expense> _expenseRepository;

    public CreateExpenseCommandHandler(
        IHttpContextAccessor httpContextAccessor,
        IBaseRepository<Expense> expenseRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _expenseRepository = expenseRepository;
    }

    public async Task<ErrorOr<int>> Handle(CreateExpenseCommand command, CancellationToken cancellationToken)
    {
        string? userIdStr = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdStr is null || string.IsNullOrWhiteSpace(userIdStr))
        {
            return Errors.Expense.ExpenseCreationFailed;
        }
        Guid userId;
        if (!Guid.TryParse(userIdStr, out userId))
        {
            return Errors.Expense.ExpenseCreationFailed;
        }

        Expense expense = Expense.Create(
            command.Amount,
            DateTime.UtcNow,
            DateTime.UtcNow,
            command.Description,
            command.CategoryId,
            userId);

        var result = await _expenseRepository.AddAsync(expense);

        return result > 0 ? result : Errors.Expense.ExpenseCreationFailed;
    }
}