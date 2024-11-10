using ErrorOr;

using MediatR;

namespace ExpenseTracker.Application.ExpenseOperations.Commands.Delete;

public record DeleteExpenseCommand(Guid Id) : IRequest<ErrorOr<bool>>;