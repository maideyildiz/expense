using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.InvestmentOperations.Common;
using ExpenseTracker.Application.Common.Errors;
using MediatR;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ExpenseTracker.Application.InvestmentOperations.Commands;

public class GetInvestmentQueryHandler : IRequestHandler<GetInvestmentQuery, ErrorOr<InvestmentResult>>
{
    private readonly IInvestmentService _investmentService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetInvestmentQueryHandler(IInvestmentService investmentService, IHttpContextAccessor httpContextAccessor)
    {
        _investmentService = investmentService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ErrorOr<InvestmentResult>> Handle(GetInvestmentQuery query, CancellationToken cancellationToken)
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
        var check = await _investmentService.CheckIfUserOwnsInvestment(query.Id, userId);
        if (!check)
        {
            return Errors.Investment.InvestmentNotFound;
        }
        var result = await _investmentService.GetInvestmentByIdAsync(query.Id);
        return result;
    }
}