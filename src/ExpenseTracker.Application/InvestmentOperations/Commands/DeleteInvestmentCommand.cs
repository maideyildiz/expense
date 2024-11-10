using ErrorOr;

using MediatR;

namespace ExpenseTracker.Application.InvestmentOperations.Commands;

public record DeleteInvestmentCommand(Guid Id) : IRequest<ErrorOr<int>>;