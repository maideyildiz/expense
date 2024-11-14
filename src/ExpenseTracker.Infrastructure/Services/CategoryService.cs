
using ExpenseTracker.Application.CategoryOperations.Common;
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.Common.Errors;
using ErrorOr;
namespace ExpenseTracker.Infrastructure.Services;


public class CategoryService : ICategoryService
{
    private readonly IInvestmentCategoryRepository _investmentCategoryRepository;
    private readonly IExpenseCategoryRepository _expenseCategoryRepository;
    public CategoryService(
        IInvestmentCategoryRepository investmentCategoryRepository,
        IExpenseCategoryRepository expenseCategoryRepository)
    {
        _investmentCategoryRepository = investmentCategoryRepository;
        _expenseCategoryRepository = expenseCategoryRepository;
    }

    public async Task<(IEnumerable<CategoryResult> Items, int TotalCount)> GetExpenseCategoriesAsync(int page, int pageSize)
    {
        return await _expenseCategoryRepository.GetExpenseCategoriesAsync(page, pageSize);
    }

    public async Task<(IEnumerable<CategoryResult> Items, int TotalCount)> GetInvestmentCategoriesAsync(int page, int pageSize)
    {
        return await _investmentCategoryRepository.GetInvestmentCategoriesAsync(page, pageSize);
    }

    public async Task<ErrorOr<CategoryResult>> GetExpenseCategoryByIdAsync(Guid id)
    {
        var expenseCategory = await _expenseCategoryRepository.GetExpenseCategoryByIdAsync(id);
        if (expenseCategory == null)
        {
            return Errors.Category.NotFound;
        }
        return expenseCategory;
    }

    public async Task<ErrorOr<CategoryResult>> GetInvestmentCategoryByIdAsync(Guid id)
    {
        var investmentCategory = await _investmentCategoryRepository.GetInvestmentCategoryByIdAsync(id);
        if (investmentCategory == null)
        {
            return Errors.Category.NotFound;
        }

        return investmentCategory;
    }
}