using ErrorOr;

using ExpenseTracker.Application.InvestmentOperations.Common;

using MediatR;

namespace ExpenseTracker.Application.InvestmentOperations.Commands.Update;

public record UpdateInvestmentCommand(
    Guid Id,
    decimal Amount,
    string Description,
    Guid CategoryId) : IRequest<ErrorOr<InvestmentResult>>;