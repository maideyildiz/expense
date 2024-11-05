using System.Security.Claims;

using ErrorOr;

using ExpenseTracker.Application.Common.Errors;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.Common.Models;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.Application.ExpenseOperations.Queries
{
    public class GetExpensesQueryHandler : IRequestHandler<GetExpensesQuery, ErrorOr<PagedResult<GetExpensesQueryResult>>>
    {
        private readonly IExpenseService _expenseService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetExpensesQueryHandler(IExpenseService expenseService, IHttpContextAccessor httpContextAccessor)
        {
            _expenseService = expenseService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ErrorOr<PagedResult<GetExpensesQueryResult>>> Handle(GetExpensesQuery request, CancellationToken cancellationToken)
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

            // Call the updated service method with pagination parameters
            var (items, totalCount) = await _expenseService.GetExpenseAsync(userId, request.Page, request.PageSize);

            return new PagedResult<GetExpensesQueryResult>(items.ToList(), totalCount, request.Page, request.PageSize);
        }

    }
}
