using MediatR;
using System.Security.Claims;
using ExpenseTracker.Application.Common.Errors;
using Microsoft.AspNetCore.Http;
using ErrorOr;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.InvestmentOperations.Common;
using ExpenseTracker.Application.Common.Base;

namespace ExpenseTracker.Application.InvestmentOperations.Commands.Create;


public class CreateInvestmentCommandHandler : BaseHandler, IRequestHandler<CreateInvestmentCommand, ErrorOr<InvestmentResult>>
{
    private readonly IInvestmentService _investmentService;

    public CreateInvestmentCommandHandler(
        IInvestmentService investmentService,
        IUserService userService)
        : base(userService)
    {
        _investmentService = investmentService;
    }

    public async Task<ErrorOr<InvestmentResult>> Handle(CreateInvestmentCommand command, CancellationToken cancellationToken)
    {
        var userIdResult = GetUserId();
        if (userIdResult.IsError)
        {
            return userIdResult.Errors;
        }

        var result = await _investmentService.AddInvestmentAsync(command, userIdResult.Value);
        if (result.IsError)
        {
            return Errors.Investment.InvestmentMustHaveACategory;
        }

        return await _investmentService.GetInvestmentByIdAsync(result.Value);
    }
}

