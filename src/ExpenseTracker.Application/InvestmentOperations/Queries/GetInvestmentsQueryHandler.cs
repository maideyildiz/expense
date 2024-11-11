using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.Common.Models;
using ExpenseTracker.Application.InvestmentOperations.Commands;
using ExpenseTracker.Application.InvestmentOperations.Common;
using ExpenseTracker.Application.Common.Errors;
using MediatR;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ExpenseTracker.Application.Common.Base;

namespace ExpenseTracker.çApplication.InvestmentOperations.Queries;


public class GetInvestmentsQueryHandler : BaseHandler, IRequestHandler<GetInvestmentsQuery, ErrorOr<PagedResult<InvestmentResult>>>
{
    private readonly IInvestmentService _investmentService;

    public GetInvestmentsQueryHandler(
        IInvestmentService investmentService,
        IUserService userService)
        : base(userService)
    {
        _investmentService = investmentService;
    }

    public async Task<ErrorOr<PagedResult<InvestmentResult>>> Handle(GetInvestmentsQuery query, CancellationToken cancellationToken)
    {
        var userIdResult = GetUserId();
        if (userIdResult.IsError)
        {
            return userIdResult.Errors;
        }
        var (items, totalCount) = await _investmentService.GetInvestmentsAsync(userIdResult.Value, query.Page, query.PageSize);

        return new PagedResult<InvestmentResult>(items.ToList(), totalCount, query.Page, query.PageSize);

    }
}