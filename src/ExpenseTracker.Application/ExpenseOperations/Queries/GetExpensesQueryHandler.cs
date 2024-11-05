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
            Guid userId;
            if (!Guid.TryParse(userIdStr, out userId))
            {
                return Errors.Expense.ExpenseCreationFailed;
            }

            var expenseResults = await _expenseService.GetExpenseAsync(userId);
            var totalCount = expenseResults.Count;
            return new PagedResult<GetExpensesQueryResult>(expenseResults, totalCount, request.Page, request.PageSize);
        }
    }
}
