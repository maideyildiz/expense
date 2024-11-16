using ExpenseTracker.Application.CategoryOperations.Common;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;

public interface IExpenseCategoryRepository : IBaseRepository<ExpenseCategory>
{
    Task<IEnumerable<CategoryResult>> GetExpenseCategoriesAsync(int page, int pageSize);
    Task<CategoryResult?> GetExpenseCategoryByIdAsync(Guid id);

}