using ExpenseTracker.Application.CategoryOperations.Common;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ErrorOr;
using MediatR;

namespace ExpenseTracker.Application.CategoryOperations.Queries;

public class GetExpenseCategoryQueryHandler : IRequestHandler<GetExpenseCategoryQuery, ErrorOr<CategoryResult>>
{
    private readonly ICategoryService _categoryService;

    public GetExpenseCategoryQueryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<ErrorOr<CategoryResult>> Handle(GetExpenseCategoryQuery query, CancellationToken cancellationToken)
    {
        return await _categoryService.GetExpenseCategoryByIdAsync(query.Id);
    }
}