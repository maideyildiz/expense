using Microsoft.AspNetCore.Http;
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Core.Entities;
using System.Security.Claims;
using ExpenseTracker.Application.Common.Errors;
using ErrorOr;
using MediatR;
using ExpenseTracker.Application.Common.Interfaces.Services;

namespace ExpenseTracker.Application.ExpenseOperations.Commands;

public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, ErrorOr<int>>
{
    private readonly IExpenseService _expenseService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateExpenseCommandHandler(
        IExpenseService expenseService,
        IHttpContextAccessor httpContextAccessor)
    {
        _expenseService = expenseService;
        _httpContextAccessor = httpContextAccessor;
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

        return await _expenseService.AddExpenseAsync(command, userId);
    }
}