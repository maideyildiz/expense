using ErrorOr;

using ExpenseTracker.Application.Common.Models;
using ExpenseTracker.Application.ExpenseOperations.Commands.Common;

using MediatR;

namespace ExpenseTracker.Application.ExpenseOperations.Queries;
public record GetExpenseQuery(Guid Id) : IRequest<ErrorOr<ExpenseResult>>;

