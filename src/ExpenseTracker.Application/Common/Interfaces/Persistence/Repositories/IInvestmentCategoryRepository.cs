using ExpenseTracker.Application.CategoryOperations.Common;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;

public interface IInvestmentCategoryRepository : IBaseRepository<InvestmentCategory>
{
    Task<IEnumerable<CategoryResult>> GetInvestmentCategoriesAsync(int page, int pageSize);
    Task<CategoryResult?> GetInvestmentCategoryByIdAsync(Guid id);
}