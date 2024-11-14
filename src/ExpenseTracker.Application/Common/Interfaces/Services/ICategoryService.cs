using ErrorOr;

using ExpenseTracker.Application.CategoryOperations.Common;

namespace ExpenseTracker.Application.Common.Interfaces.Services;

public interface ICategoryService
{
    Task<(IEnumerable<CategoryResult> Items, int TotalCount)> GetExpenseCategoriesAsync(int page, int pageSize);
    Task<(IEnumerable<CategoryResult> Items, int TotalCount)> GetInvestmentCategoriesAsync(int page, int pageSize);
    Task<ErrorOr<CategoryResult>> GetExpenseCategoryByIdAsync(Guid id);
    Task<ErrorOr<CategoryResult>> GetInvestmentCategoryByIdAsync(Guid id);
}