using ErrorOr;

using ExpenseTracker.Application.CategoryOperations.Common;
using ExpenseTracker.Application.Common.Models;

using MediatR;

namespace ExpenseTracker.Application.CategoryOperations.Queries;

public record GetInvestmentCategoriesQuery(int Page, int PageSize) : IRequest<ErrorOr<PagedResult<GetInvestmentCategoryResult>>>;