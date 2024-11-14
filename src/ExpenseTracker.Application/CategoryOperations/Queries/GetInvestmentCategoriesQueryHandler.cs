using ExpenseTracker.Application.CategoryOperations.Common;
using ExpenseTracker.Application.Common.Models;
using ErrorOr;
using MediatR;
using ExpenseTracker.Application.Common.Interfaces.Services;

namespace ExpenseTracker.Application.CategoryOperations.Queries;

public class GetInvestmentCategoriesQueryHandler : IRequestHandler<GetInvestmentCategoriesQuery, ErrorOr<PagedResult<CategoryResult>>>
{
    private readonly ICategoryService _categoryService;

    public GetInvestmentCategoriesQueryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<ErrorOr<PagedResult<CategoryResult>>> Handle(GetInvestmentCategoriesQuery query, CancellationToken cancellationToken)
    {
        var (items, totalCount) = await _categoryService.GetInvestmentCategoriesAsync(query.Page, query.PageSize);

        return new PagedResult<CategoryResult>(items.ToList(), totalCount, query.Page, query.PageSize);
    }
}