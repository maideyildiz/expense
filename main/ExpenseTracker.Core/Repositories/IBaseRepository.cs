using ExpenseTracker.Core.Models;
namespace ExpenseTracker.Core.Repositories;

public interface IBaseRepository<T> where T : Base
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<int> AddAsync(T obj);
    Task<int> UpdateAsync(T obj);
    Task<int> DeleteAsync(Guid id);
}