using ExpenseTracker.Application.CategoryOperations.Common;
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.Common.Errors;
using ErrorOr;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using ExpenseTracker.Application.Common.Interfaces.Cache;
namespace ExpenseTracker.Infrastructure.Services;


public class CategoryService : ICategoryService
{
    private readonly ICacheService _redisCacheService;
    private readonly IInvestmentCategoryRepository _investmentCategoryRepository;
    private readonly IExpenseCategoryRepository _expenseCategoryRepository;
    public CategoryService(
        IInvestmentCategoryRepository investmentCategoryRepository,
        IExpenseCategoryRepository expenseCategoryRepository,
        ICacheService redisCacheService)
    {
        _investmentCategoryRepository = investmentCategoryRepository;
        _expenseCategoryRepository = expenseCategoryRepository;
        _redisCacheService = redisCacheService;
    }

    public async Task<(IEnumerable<CategoryResult> Items, int TotalCount)> GetExpenseCategoriesAsync(int page, int pageSize)
    {
        string cacheKey = $"ExpenseCategories_{page}_{pageSize}";

        var cachedData = await _redisCacheService.GetAsync<IEnumerable<CategoryResult>>(cacheKey);
        if (cachedData != null && cachedData.Any())
        {
            return (cachedData, cachedData.Count());
        }

        var data = await _expenseCategoryRepository.GetExpenseCategoriesAsync(page, pageSize);
        await _redisCacheService.SetAsync(cacheKey, data);
        return (data, data.Count());
    }

    public async Task<(IEnumerable<CategoryResult> Items, int TotalCount)> GetInvestmentCategoriesAsync(int page, int pageSize)
    {
        string cacheKey = $"InvestmentCategories_{page}_{pageSize}";
        var cachedData = await _redisCacheService.GetAsync<IEnumerable<CategoryResult>>(cacheKey);
        if (cachedData != null && cachedData.Any())
        {
            return (cachedData, cachedData.Count());
        }

        var data = await _investmentCategoryRepository.GetInvestmentCategoriesAsync(page, pageSize);
        await _redisCacheService.SetAsync(cacheKey, data);
        return (data, data.Count());
    }

    public async Task<ErrorOr<CategoryResult>> GetExpenseCategoryByIdAsync(Guid id)
    {
        string cacheKey = $"ExpenseCategoryById_{id}";

        var cachedData = await _redisCacheService.GetAsync<CategoryResult>(cacheKey);
        if (cachedData != null)
        {
            return cachedData;
        }

        var expenseCategory = await _expenseCategoryRepository.GetExpenseCategoryByIdAsync(id);
        if (expenseCategory == null)
        {
            return Errors.Category.NotFound;
        }

        await _redisCacheService.SetAsync(cacheKey, expenseCategory);

        return expenseCategory;
    }

    public async Task<ErrorOr<CategoryResult>> GetInvestmentCategoryByIdAsync(Guid id)
    {
        string cacheKey = $"InvestmentCategoryById_{id}";
        var cachedData = await _redisCacheService.GetAsync<CategoryResult>(cacheKey);
        if (cachedData != null)
        {
            return cachedData;
        }
        var investmentCategory = await _investmentCategoryRepository.GetInvestmentCategoryByIdAsync(id);
        if (investmentCategory == null)
        {
            return Errors.Category.NotFound;
        }
        await _redisCacheService.SetAsync(cacheKey, investmentCategory);

        return investmentCategory;
    }
}