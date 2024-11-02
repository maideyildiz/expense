using ErrorOr;

using MediatR;
namespace ExpenseTracker.Application.ExpenseOperations.Commands;
public record CreateExpenseCommand(
    decimal Amount,
    string Description,
    Guid CategoryId) : IRequest<ErrorOr<int>>;