
using ErrorOr;

using MediatR;
using System.Security.Claims;
using ExpenseTracker.Application.Common.Errors;
using Microsoft.AspNetCore.Http;
using ExpenseTracker.Application.Common.Interfaces.Services;

namespace ExpenseTracker.Application.InvestmentOperations.Commands.Create;


public class CreateInvestmentCommandHandler : IRequestHandler<CreateInvestmentCommand, ErrorOr<int>>
{
    private readonly IInvestmentService _investmentService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CreateInvestmentCommandHandler(IHttpContextAccessor httpContextAccessor, IInvestmentService investmentService)
    {
        _httpContextAccessor = httpContextAccessor;
        _investmentService = investmentService;
    }
    public async Task<ErrorOr<int>> Handle(CreateInvestmentCommand request, CancellationToken cancellationToken)
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

        return await _investmentService.AddInvestmentAsync(request, userId);
    }
}
