using Microsoft.AspNetCore.Http;
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Core.Entities;
using System.Security.Claims;
using ExpenseTracker.Application.Common.Errors;
using ErrorOr;
using MediatR;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.ExpenseOperations.Commands.Common;
using ExpenseTracker.Application.Common.Base;

namespace ExpenseTracker.Application.ExpenseOperations.Commands.Create;

public class CreateExpenseCommandHandler : BaseHandler, IRequestHandler<CreateExpenseCommand, ErrorOr<ExpenseResult>>
{
    private readonly IExpenseService _expenseService;

    public CreateExpenseCommandHandler(
        IExpenseService expenseService,
        IUserService userService)
        : base(userService)
    {
        _expenseService = expenseService;
    }

    public async Task<ErrorOr<ExpenseResult>> Handle(CreateExpenseCommand command, CancellationToken cancellationToken)
    {
        var userIdResult = GetUserId();
        if (userIdResult.IsError)
        {
            return userIdResult.Errors;
        }
        var result = await _expenseService.AddExpenseAsync(command, userIdResult.Value);
        if (result.IsError)
        {
            return Errors.Expense.ExpenseCreationFailed;
        }
        return await _expenseService.GetExpenseByIdAsync(result.Value);
    }
}