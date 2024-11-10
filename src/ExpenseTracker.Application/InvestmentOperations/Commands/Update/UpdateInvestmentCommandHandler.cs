
using ErrorOr;

using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.InvestmentOperations.Common;
using System.Security.Claims;
using MediatR;
using ExpenseTracker.Application.Common.Errors;
using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.Application.InvestmentOperations.Commands.Update;

public class UpdateInvestmentCommandHandler : IRequestHandler<UpdateInvestmentCommand, ErrorOr<InvestmentResult>>
{
    private readonly IInvestmentService _investmentService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateInvestmentCommandHandler(IInvestmentService investmentService, IHttpContextAccessor httpContextAccessor)
    {
        _investmentService = investmentService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ErrorOr<InvestmentResult>> Handle(UpdateInvestmentCommand command, CancellationToken cancellationToken)
    {
        string? userIdStr = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdStr is null || string.IsNullOrWhiteSpace(userIdStr))
        {
            return Errors.Investment.InvestmentUpdateFailed;
        }

        if (!Guid.TryParse(userIdStr, out Guid userId))
        {
            return Errors.Investment.InvestmentUpdateFailed;
        }
        var check = await _investmentService.CheckIfUserOwnsInvestment(command.Id, userId);
        if (!check)
        {
            return Errors.Investment.InvestmentNotFound;
        }
        return await _investmentService.UpdateInvestmentAsync(command);
    }
}