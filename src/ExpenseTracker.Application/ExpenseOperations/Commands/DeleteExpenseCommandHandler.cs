
using System.Security.Claims;

using ErrorOr;

using ExpenseTracker.Application.Common.Errors;
using ExpenseTracker.Application.Common.Interfaces.Services;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.Application.ExpenseOperations.Commands;

public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand, ErrorOr<int>>
{
    private readonly IExpenseService _expenseService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public DeleteExpenseCommandHandler(IExpenseService expenseService, IHttpContextAccessor httpContextAccessor)
    {
        _expenseService = expenseService;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<ErrorOr<int>> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
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
        var expense = await _expenseService.GetExpenseByIdAsync(request.Id);
        if (expense.IsError)
        {
            return Errors.Expense.ExpenseNotFound;
        }

        return await _expenseService.DeleteExpenseAsync(request.Id);

        throw new NotImplementedException();
    }
}
