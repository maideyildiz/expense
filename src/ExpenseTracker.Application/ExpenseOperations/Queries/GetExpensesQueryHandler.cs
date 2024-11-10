using System.Security.Claims;

using ErrorOr;

using ExpenseTracker.Application.Common.Errors;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.Common.Models;
using ExpenseTracker.Application.ExpenseOperations.Commands.Common;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.Application.ExpenseOperations.Queries
{
    public class GetExpensesQueryHandler : IRequestHandler<GetExpensesQuery, ErrorOr<PagedResult<ExpenseResult>>>
    {
        private readonly IExpenseService _expenseService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetExpensesQueryHandler(IExpenseService expenseService, IHttpContextAccessor httpContextAccessor)
        {
            _expenseService = expenseService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ErrorOr<PagedResult<ExpenseResult>>> Handle(GetExpensesQuery query, CancellationToken cancellationToken)
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
            var (items, totalCount) = await _expenseService.GetExpensesAsync(userId, query.Page, query.PageSize);

            return new PagedResult<ExpenseResult>(items.ToList(), totalCount, query.Page, query.PageSize);
        }

    }
}
