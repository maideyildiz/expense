using ExpenseTracker.Core.Common.Base;

namespace ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
public interface IBaseRepository<T>
    where T : EntityBase<Guid>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllByQueryAsync(string query, object? param = null);
    Task<int> AddAsync(T obj);
    Task<int> UpdateAsync(T obj);
    Task<int> DeleteAsync(Guid id);
}