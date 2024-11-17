using ExpenseTracker.Application.CategoryOperations.Common;
using ExpenseTracker.Application.Common.Models;
using ErrorOr;
using MediatR;
using ExpenseTracker.Application.Common.Interfaces.Services;

namespace ExpenseTracker.Application.CategoryOperations.Queries;

public class GetInvestmentCategoryQueryHandler : IRequestHandler<GetCategoryQuery, ErrorOr<CategoryResult>>
{
    private readonly ICategoryService _categoryService;

    public GetInvestmentCategoryQueryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<ErrorOr<CategoryResult>> Handle(GetCategoryQuery query, CancellationToken cancellationToken)
    {
        return await _categoryService.GetInvestmentCategoryByIdAsync(query.Id);
    }
}