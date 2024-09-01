using ExpenseTracker.Core.Models;
namespace ExpenseTracker.Core.Services;

public interface IBaseService<T> where T : Base
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T obj);
    Task UpdateAsync(T obj);
    Task DeleteAsync(int id);
}