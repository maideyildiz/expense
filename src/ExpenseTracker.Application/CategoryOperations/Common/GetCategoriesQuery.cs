using ErrorOr;

using ExpenseTracker.Application.Common.Models;

using MediatR;

namespace ExpenseTracker.Application.CategoryOperations.Common;

public record GetCategoriesQuery(int Page, int PageSize) : IRequest<ErrorOr<PagedResult<CategoryResult>>>;