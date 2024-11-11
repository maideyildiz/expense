using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.InvestmentOperations.Common;
using ExpenseTracker.Application.Common.Errors;
using MediatR;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ExpenseTracker.Application.Common.Base;

namespace ExpenseTracker.Application.InvestmentOperations.Commands;

public class GetInvestmentQueryHandler : BaseHandler, IRequestHandler<GetInvestmentQuery, ErrorOr<InvestmentResult>>
{
    private readonly IInvestmentService _investmentService;

    public GetInvestmentQueryHandler(
        IInvestmentService investmentService,
        IUserService userService)
        : base(userService)
    {
        _investmentService = investmentService;
    }

    public async Task<ErrorOr<InvestmentResult>> Handle(GetInvestmentQuery query, CancellationToken cancellationToken)
    {
        var userIdResult = GetUserId();
        if (userIdResult.IsError)
        {
            return userIdResult.Errors;
        }
        var check = await _investmentService.CheckIfUserOwnsInvestment(userIdResult.Value, query.Id);
        if (!check)
        {
            return Errors.Investment.InvestmentNotFound;
        }
        var result = await _investmentService.GetInvestmentByIdAsync(query.Id);
        return result;
    }
}