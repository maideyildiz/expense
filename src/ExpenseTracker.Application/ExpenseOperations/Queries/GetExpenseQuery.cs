using ErrorOr;

using ExpenseTracker.Application.Common.Models;

using MediatR;

namespace ExpenseTracker.Application.ExpenseOperations.Queries;
public record GetExpenseQuery(Guid Id) : IRequest<ErrorOr<GetExpenseQueryResult>>;

