using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.Common.Errors;
using MediatR;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ErrorOr;
namespace ExpenseTracker.Application.InvestmentOperations.Commands.Delete;


public class DeleteInvestmentCommandHandler : IRequestHandler<DeleteInvestmentCommand, ErrorOr<bool>>
{
    private readonly IInvestmentService _investmentService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DeleteInvestmentCommandHandler(IInvestmentService investmentService, IHttpContextAccessor httpContextAccessor)
    {
        _investmentService = investmentService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ErrorOr<bool>> Handle(DeleteInvestmentCommand command, CancellationToken cancellationToken)
    {
        var userIdStr = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdStr is null || string.IsNullOrWhiteSpace(userIdStr))
        {
            return Errors.Investment.InvestmentUpdateFailed;
        }
        Guid userId;
        if (!Guid.TryParse(userIdStr, out userId))
        {
            return Errors.Investment.InvestmentUpdateFailed;
        }
        var check = await _investmentService.CheckIfUserOwnsInvestment(command.Id, userId);
        if (!check)
        {
            return Errors.Investment.InvestmentNotFound;
        }

        return await _investmentService.DeleteInvestmentAsync(command.Id);
    }
}