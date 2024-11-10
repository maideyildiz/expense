using ErrorOr;

using ExpenseTracker.Application.InvestmentOperations.Common;

using MediatR;

namespace ExpenseTracker.Application.InvestmentOperations.Commands;


public record GetInvestmentQuery(Guid Id) : IRequest<ErrorOr<InvestmentResult>>;