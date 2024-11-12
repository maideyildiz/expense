using ExpenseTracker.Application.Common.Errors;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.Common.Models;
using ErrorOr;
using ExpenseTracker.Application.ExpenseOperations.Commands.Common;
using Microsoft.AspNetCore.Http;
using MediatR;
using System.Security.Claims;
using ExpenseTracker.Application.Common.Base;


namespace ExpenseTracker.Application.ExpenseOperations.Queries
{
    public class GetExpenseQueryHandler : BaseHandler, IRequestHandler<GetExpenseQuery, ErrorOr<ExpenseResult>>
    {
        private readonly IExpenseService _expenseService;

        public GetExpenseQueryHandler(
            IExpenseService expenseService,
            IUserService userService)
            : base(userService)
        {
            _expenseService = expenseService;
        }

        public async Task<ErrorOr<ExpenseResult>> Handle(GetExpenseQuery query, CancellationToken cancellationToken)
        {
            var userIdResult = GetUserId();
            if (userIdResult.IsError)
            {
                return userIdResult.Errors;
            }
            var check = await _expenseService.CheckIfUserOwnsExpense(userIdResult.Value, query.Id);
            if (!check)
            {
                return Errors.Expense.ExpenseNotFound;
            }
            var result = await _expenseService.GetExpenseByIdAsync(query.Id);
            return result;
        }
    }
}
