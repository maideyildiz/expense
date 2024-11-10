
using ErrorOr;

using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.Common.Errors;
using MediatR;

using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ExpenseTracker.Application.ExpenseOperations.Commands.Common;

namespace ExpenseTracker.Application.ExpenseOperations.Commands;


public class UpdateExpenseHandler : IRequestHandler<UpdateExpenseCommand, ErrorOr<ExpenseResult>>
{
    private readonly IExpenseService _expenseService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UpdateExpenseHandler(
        IExpenseService expenseService,
        IHttpContextAccessor httpContextAccessor)
    {
        _expenseService = expenseService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ErrorOr<ExpenseResult>> Handle(UpdateExpenseCommand command, CancellationToken cancellationToken)
    {
        string? userIdStr = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdStr is null || string.IsNullOrWhiteSpace(userIdStr))
        {
            return Errors.Expense.ExpenseCreationFailed;
        }

        if (!Guid.TryParse(userIdStr, out Guid userId))
        {
            return Errors.Expense.ExpenseCreationFailed;
        }

        return await _expenseService.UpdateExpenseAsync(command);
    }
}