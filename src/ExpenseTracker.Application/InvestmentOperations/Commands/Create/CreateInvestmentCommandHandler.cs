using MediatR;
using System.Security.Claims;
using ExpenseTracker.Application.Common.Errors;
using Microsoft.AspNetCore.Http;
using ErrorOr;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.InvestmentOperations.Common;

namespace ExpenseTracker.Application.InvestmentOperations.Commands.Create;


public class CreateInvestmentCommandHandler : IRequestHandler<CreateInvestmentCommand, ErrorOr<InvestmentResult>>
{
    private readonly IInvestmentService _investmentService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CreateInvestmentCommandHandler(IHttpContextAccessor httpContextAccessor, IInvestmentService investmentService)
    {
        _httpContextAccessor = httpContextAccessor;
        _investmentService = investmentService;
    }
    public async Task<ErrorOr<InvestmentResult>> Handle(CreateInvestmentCommand command, CancellationToken cancellationToken)
    {
        string? userIdStr = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdStr is null || string.IsNullOrWhiteSpace(userIdStr))
        {
            return Errors.Investment.InvestmentMustHaveACategory;
        }
        Guid userId;
        if (!Guid.TryParse(userIdStr, out userId))
        {
            return Errors.Investment.InvestmentMustHaveACategory;
        }

        var result = await _investmentService.AddInvestmentAsync(command, userId);
        if (result.IsError)
        {
            return Errors.Investment.InvestmentMustHaveACategory;
        }
        return await _investmentService.GetInvestmentByIdAsync(result.Value);
    }
}
