using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ExpenseTracker.Application.ExpenseOperations.Commands.Common;
using ExpenseTracker.Application.Common.Base;
using ErrorOr;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.Common.Errors;
using MediatR;
namespace ExpenseTracker.Application.ExpenseOperations.Commands.Update;


public class UpdateExpenseCommandHandler : BaseHandler, IRequestHandler<UpdateExpenseCommand, ErrorOr<ExpenseResult>>
{
    private readonly IExpenseService _expenseService;
    public UpdateExpenseCommandHandler(
        IExpenseService expenseService,
        IUserService userService)
        : base(userService)
    {
        _expenseService = expenseService;
    }

    public async Task<ErrorOr<ExpenseResult>> Handle(UpdateExpenseCommand command, CancellationToken cancellationToken)
    {
        var userIdResult = GetUserId();
        if (userIdResult.IsError)
        {
            return userIdResult.Errors;
        }
        var check = await _expenseService.CheckIfUserOwnsExpense(command.Id, userIdResult.Value);
        if (!check)
        {
            return Errors.Expense.ExpenseNotFound;
        }
        return await _expenseService.UpdateExpenseAsync(command);
    }
}