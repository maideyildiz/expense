using ErrorOr;

using MediatR;

namespace ExpenseTracker.Application.CategoryOperations.Common;


public record GetCategoryQuery(Guid Id) : IRequest<ErrorOr<CategoryResult>>;