using MediatR;
using ExpenseTracker.Application.Common.Base;
using ExpenseTracker.Application.Common.Errors;
using ErrorOr;
using ExpenseTracker.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ExpenseTracker.Application.ExpenseOperations.Commands.Delete;

public class DeleteExpenseCommandHandler : BaseHandler, IRequestHandler<DeleteExpenseCommand, ErrorOr<bool>>
{
    private readonly IExpenseService _expenseService;
    public DeleteExpenseCommandHandler(
        IExpenseService expenseService,
        IUserService userService)
        : base(userService)
    {
        _expenseService = expenseService;
    }
    public async Task<ErrorOr<bool>> Handle(DeleteExpenseCommand command, CancellationToken cancellationToken)
    {
        var userIdResult = GetUserId();
        if (userIdResult.IsError)
        {
            return userIdResult.Errors;
        }
        var check = await _expenseService.CheckIfUserOwnsExpense(userIdResult.Value, command.Id);
        if (!check)
        {
            return Errors.Expense.ExpenseNotFound;
        }

        return await _expenseService.DeleteExpenseAsync(command.Id);
    }
}
