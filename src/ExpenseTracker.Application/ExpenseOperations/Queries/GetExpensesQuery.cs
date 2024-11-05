using ErrorOr;

using ExpenseTracker.Application.Common.Models;

using MediatR;

namespace ExpenseTracker.Application.ExpenseOperations.Queries;
public record GetExpensesQuery(int Page, int PageSize) : IRequest<ErrorOr<PagedResult<GetExpensesQueryResult>>>;

