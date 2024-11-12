using ErrorOr;

using ExpenseTracker.Application.CategoryOperations.Common;

using MediatR;

namespace ExpenseTracker.Application.CategoryOperations.Queries;


public record GetExpenseCategoryQuery(Guid Id) : IRequest<ErrorOr<GetExpenseCategoryResult>>;