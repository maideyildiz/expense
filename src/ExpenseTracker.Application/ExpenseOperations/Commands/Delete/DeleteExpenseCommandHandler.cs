
using System.Security.Claims;

using ErrorOr;

using ExpenseTracker.Application.Common.Errors;
using ExpenseTracker.Application.Common.Interfaces.Services;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.Application.ExpenseOperations.Commands.Delete;

public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand, ErrorOr<bool>>
{
    private readonly IExpenseService _expenseService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public DeleteExpenseCommandHandler(IExpenseService expenseService, IHttpContextAccessor httpContextAccessor)
    {
        _expenseService = expenseService;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<ErrorOr<bool>> Handle(DeleteExpenseCommand command, CancellationToken cancellationToken)
    {
        var userIdStr = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdStr is null || string.IsNullOrWhiteSpace(userIdStr))
        {
            return Errors.Expense.ExpenseUpdateFailed;
        }
        Guid userId;
        if (!Guid.TryParse(userIdStr, out userId))
        {
            return Errors.Expense.ExpenseUpdateFailed;
        }
        var check = await _expenseService.CheckIfUserOwnsExpense(command.Id, userId);
        if (!check)
        {
            return Errors.Expense.ExpenseNotFound;
        }

        return await _expenseService.DeleteExpenseAsync(command.Id);
    }
}
