using ErrorOr;

using ExpenseTracker.Application.InvestmentOperations.Common;

using MediatR;

namespace ExpenseTracker.Application.InvestmentOperations.Commands.Create;


public record CreateInvestmentCommand(
    string Name,
    decimal Amount,
    Guid CategoryId) : IRequest<ErrorOr<InvestmentResult>>;