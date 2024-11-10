using ErrorOr;

using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.Common.Models;
using ExpenseTracker.Application.InvestmentOperations.Commands;
using ExpenseTracker.Application.InvestmentOperations.Common;
using ExpenseTracker.Application.Common.Errors;
using MediatR;

using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ExpenseTracker.çApplication.InvestmentOperations.Queries;


public class GetInvestmentsQueryHandler : IRequestHandler<GetInvestmentsQuery, ErrorOr<PagedResult<InvestmentResult>>>
{
    private readonly IInvestmentService _investmentService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetInvestmentsQueryHandler(IInvestmentService ınvestmentService, IHttpContextAccessor httpContextAccessor)
    {
        _investmentService = ınvestmentService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ErrorOr<PagedResult<InvestmentResult>>> Handle(GetInvestmentsQuery query, CancellationToken cancellationToken)
    {
        string? userIdStr = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdStr is null || string.IsNullOrWhiteSpace(userIdStr))
        {
            return Errors.Investment.InvestmentCreationFailed;
        }

        if (!Guid.TryParse(userIdStr, out Guid userId))
        {
            return Errors.Investment.InvestmentCreationFailed;
        }
        var (items, totalCount) = await _investmentService.GetInvestmentsAsync(userId, query.Page, query.PageSize);

        return new PagedResult<InvestmentResult>(items.ToList(), totalCount, query.Page, query.PageSize);

    }
}