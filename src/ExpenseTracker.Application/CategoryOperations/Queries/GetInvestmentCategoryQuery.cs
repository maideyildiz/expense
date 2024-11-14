using ErrorOr;

using ExpenseTracker.Application.CategoryOperations.Common;

using MediatR;

namespace ExpenseTracker.Application.CategoryOperations.Queries;


public record GetInvestmentCategoryQuery(Guid Id) : IRequest<ErrorOr<CategoryResult>>;