using ExpenseTracker.Application.CategoryOperations.Common;
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.Common.Errors;
using ErrorOr;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
namespace ExpenseTracker.Infrastructure.Services;


public class CategoryService : ICategoryService
{
    private readonly IDistributedCache _cache;
    private readonly IInvestmentCategoryRepository _investmentCategoryRepository;
    private readonly IExpenseCategoryRepository _expenseCategoryRepository;
    public CategoryService(
        IInvestmentCategoryRepository investmentCategoryRepository,
        IExpenseCategoryRepository expenseCategoryRepository,
        IDistributedCache cache)
    {
        _investmentCategoryRepository = investmentCategoryRepository;
        _expenseCategoryRepository = expenseCategoryRepository;
        _cache = cache;
    }

    public async Task<(IEnumerable<CategoryResult> Items, int TotalCount)> GetExpenseCategoriesAsync(int page, int pageSize)
    {
        // string key = $"ExpenseCategories_{page}_{pageSize}";

        // var cachedData = await _cache.GetStringAsync(key);
        // if (!string.IsNullOrEmpty(cachedData))
        // {
        //     var cachedResult = JsonSerializer.Deserialize<(IEnumerable<CategoryResult> Items, int TotalCount)>(cachedData);
        //     if (cachedResult.Items.Any())
        //     {
        //         return cachedResult;
        //     }
        // }

        var data = await _expenseCategoryRepository.GetExpenseCategoriesAsync(page, pageSize);

        // var serializedData = JsonSerializer.Serialize(data);
        // await _cache.SetStringAsync(key, serializedData, new DistributedCacheEntryOptions
        // {
        //     AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
        // });

        return data;
    }

    public async Task<(IEnumerable<CategoryResult> Items, int TotalCount)> GetInvestmentCategoriesAsync(int page, int pageSize)
    {
        string key = $"InvestmentCategories_{page}_{pageSize}";

        var cachedData = await _cache.GetStringAsync(key);
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedResult = JsonSerializer.Deserialize<(IEnumerable<CategoryResult> Items, int TotalCount)>(cachedData);
            if (cachedResult.Items.Any())
            {
                return cachedResult;
            }
        }

        var data = await _investmentCategoryRepository.GetInvestmentCategoriesAsync(page, pageSize);
        var options = new JsonSerializerOptions
        {
            WriteIndented = true // Daha okunabilir JSON çıktısı için
        };

        var serializedDatass = JsonSerializer.Serialize(data, options);
        var serializedData = JsonSerializer.Serialize(data);
        await _cache.SetStringAsync(key, serializedData, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
        });

        return data;
    }

    public async Task<ErrorOr<CategoryResult>> GetExpenseCategoryByIdAsync(Guid id)
    {
        string key = $"ExpenseCategoryById_{id}";

        var cachedData = await _cache.GetStringAsync(key);
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedResult = JsonSerializer.Deserialize<CategoryResult>(cachedData);
            if (cachedResult != null)
            {
                return cachedResult;
            }
        }

        var data = await _expenseCategoryRepository.GetExpenseCategoryByIdAsync(id);
        if (data == null)
        {
            return Errors.Category.NotFound;
        }

        var serializedData = JsonSerializer.Serialize(data);
        await _cache.SetStringAsync(key, serializedData, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
        });

        return data;
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