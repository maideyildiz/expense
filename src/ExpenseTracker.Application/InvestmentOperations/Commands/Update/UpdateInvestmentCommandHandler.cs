using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.InvestmentOperations.Common;
using System.Security.Claims;
using MediatR;
using ExpenseTracker.Application.Common.Errors;
using Microsoft.AspNetCore.Http;
using ExpenseTracker.Application.Common.Base;
using ErrorOr;

namespace ExpenseTracker.Application.InvestmentOperations.Commands.Update;

public class UpdateInvestmentCommandHandler : BaseHandler, IRequestHandler<UpdateInvestmentCommand, ErrorOr<InvestmentResult>>
{
    private readonly IInvestmentService _investmentService;

    public UpdateInvestmentCommandHandler(
        IInvestmentService investmentService,
        IUserService userService)
        : base(userService)
    {
        _investmentService = investmentService;
    }

    public async Task<ErrorOr<InvestmentResult>> Handle(UpdateInvestmentCommand command, CancellationToken cancellationToken)
    {
        var userIdResult = GetUserId();
        if (userIdResult.IsError)
        {
            return userIdResult.Errors;
        }
        var check = await _investmentService.CheckIfUserOwnsInvestment(userIdResult.Value, command.Id);
        if (!check)
        {
            return Errors.Investment.InvestmentNotFound;
        }
        return await _investmentService.UpdateInvestmentAsync(command);
    }
}