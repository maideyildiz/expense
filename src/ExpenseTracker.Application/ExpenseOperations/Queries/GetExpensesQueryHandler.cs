using ExpenseTracker.Application.Common.Errors;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.Common.Models;
using ExpenseTracker.Application.ExpenseOperations.Commands.Common;
using System.Security.Claims;
using MediatR;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using ExpenseTracker.Application.Common.Base;

namespace ExpenseTracker.Application.ExpenseOperations.Queries
{
    public class GetExpensesQueryHandler : BaseHandler, IRequestHandler<GetExpensesQuery, ErrorOr<PagedResult<ExpenseResult>>>
    {
        private readonly IExpenseService _expenseService;

        public GetExpensesQueryHandler(
            IExpenseService expenseService,
            IUserService userService)
            : base(userService)
        {
            _expenseService = expenseService;
        }

        public async Task<ErrorOr<PagedResult<ExpenseResult>>> Handle(GetExpensesQuery query, CancellationToken cancellationToken)
        {
            var userIdResult = GetUserId();
            if (userIdResult.IsError)
            {
                return userIdResult.Errors;
            }
            var (items, totalCount) = await _expenseService.GetExpensesAsync(userIdResult.Value, query.Page, query.PageSize);

            return new PagedResult<ExpenseResult>(items.ToList(), totalCount, query.Page, query.PageSize);
        }

    }
}
