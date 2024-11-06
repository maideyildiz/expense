using ErrorOr;

using MediatR;
namespace ExpenseTracker.Application.ExpenseOperations.Commands;
public record UpdateExpenseCommand(
    Guid Id,
    decimal Amount,
    string Description,
    Guid CategoryId) : IRequest<ErrorOr<UpdateExpenseResult>>;