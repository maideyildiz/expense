using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.Common.Errors;
using MediatR;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ErrorOr;
using ExpenseTracker.Application.Common.Base;
namespace ExpenseTracker.Application.InvestmentOperations.Commands.Delete;


public class DeleteInvestmentCommandHandler : BaseHandler, IRequestHandler<DeleteInvestmentCommand, ErrorOr<bool>>
{
    private readonly IInvestmentService _investmentService;

    public DeleteInvestmentCommandHandler(
        IInvestmentService investmentService,
        IUserService userService)
        : base(userService)
    {
        _investmentService = investmentService;
    }

    public async Task<ErrorOr<bool>> Handle(DeleteInvestmentCommand command, CancellationToken cancellationToken)
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

        return await _investmentService.DeleteInvestmentAsync(command.Id);
    }
}