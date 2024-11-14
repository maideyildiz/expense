using ExpenseTracker.Application.CategoryOperations.Common;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ErrorOr;
using MediatR;
using ExpenseTracker.Application.Common.Models;

namespace ExpenseTracker.Application.CategoryOperations.Queries;


public class GetExpenseCategoriesQueryHandler : IRequestHandler<GetExpenseCategoriesQuery, ErrorOr<PagedResult<CategoryResult>>>
{
    private readonly ICategoryService _categoryService;

    public GetExpenseCategoriesQueryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<ErrorOr<PagedResult<CategoryResult>>> Handle(GetExpenseCategoriesQuery query, CancellationToken cancellationToken)
    {
        var (items, totalCount) = await _categoryService.GetExpenseCategoriesAsync(query.Page, query.PageSize);

        return new PagedResult<CategoryResult>(items.ToList(), totalCount, query.Page, query.PageSize);
    }
}