namespace ExpenseTracker.Application.Common.Interfaces.Persistence;
public interface IBaseRepository<T>
    where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<int> AddAsync(T obj);
    Task<int> UpdateAsync(T obj);
    Task<int> DeleteAsync(Guid id);
}