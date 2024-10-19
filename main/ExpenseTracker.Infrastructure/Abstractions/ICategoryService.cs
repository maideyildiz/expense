using ExpenseTracker.Core.Models;
using ExpenseTracker.Core.Repositories;
namespace ExpenseTracker.Infrastructure.Abstractions;

public interface ICategoryService : IBaseRepository<Category>
{
    Task<Category> AddCategoryIfNotExistsAsync(string categoryName);
}
