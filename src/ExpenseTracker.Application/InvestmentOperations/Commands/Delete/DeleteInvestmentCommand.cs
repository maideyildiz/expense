using ErrorOr;

using MediatR;

namespace ExpenseTracker.Application.InvestmentOperations.Commands.Delete;

public record DeleteInvestmentCommand(Guid Id) : IRequest<ErrorOr<bool>>;