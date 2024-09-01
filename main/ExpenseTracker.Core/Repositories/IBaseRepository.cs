using ExpenseTracker.Core.Models;
namespace ExpenseTracker.Core.Repositories;

public interface IBaseRepository<T> where T : Base
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T obj);
    Task UpdateAsync(T obj);
    Task DeleteAsync(int id);
}