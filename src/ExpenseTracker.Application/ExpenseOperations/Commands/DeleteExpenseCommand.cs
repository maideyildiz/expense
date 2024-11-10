using ErrorOr;

using MediatR;

namespace ExpenseTracker.Application.ExpenseOperations.Commands;

public record DeleteExpenseCommand(Guid Id) : IRequest<ErrorOr<int>>;