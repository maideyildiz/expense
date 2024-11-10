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
    public class GetExpenseQueryHandler : IRequestHandler<GetExpenseQuery, ErrorOr<ExpenseResult>>
    {
        private readonly IExpenseService _expenseService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetExpenseQueryHandler(IExpenseService expenseService, IHttpContextAccessor httpContextAccessor)
        {
            _expenseService = expenseService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ErrorOr<ExpenseResult>> Handle(GetExpenseQuery request, CancellationToken cancellationToken)
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

            var result = await _expenseService.GetExpenseByIdAsync(request.Id);
            return result;
        }
    }
}
