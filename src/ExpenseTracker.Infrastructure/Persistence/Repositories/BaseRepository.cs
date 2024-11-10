using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Core.Common.Base;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Infrastructure.Persistence.Repositories;

public class BaseRepository<T> : IBaseRepository<T>
    where T : EntityBase<Guid>
{
    private readonly IDbRepository _dbRepository;

    public BaseRepository(IDbRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        string sql = "SELECT * FROM " + typeof(T).Name + "s";
        return await _dbRepository.QueryAsync<T>(sql);
    }
    public async Task<IEnumerable<T>> GetAllByQueryAsync(string query, object? param = null)
    {
        return await _dbRepository.QueryAsync<T>(query, param);
    }
    public async Task<T?> GetByIdAsync(Guid id)
    {
        string sql = $"SELECT * FROM {typeof(T).Name}s WHERE Id = @Id";
        return await _dbRepository.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
    }

    public async Task<int> AddAsync(T obj)
    {
        string insertClause = string.Join(", ", typeof(T).GetProperties().Select(p => p.Name));
        string valuesClause = string.Join(", ", typeof(T).GetProperties().Select(p => "@" + p.Name));

        string sql = $"INSERT INTO {typeof(T).Name}s ({insertClause}) VALUES ({valuesClause})";
        return await _dbRepository.ExecuteAsync(sql, obj);
    }

    public async Task<int> UpdateAsync(T obj)
    {
        string updateClause = string.Join(", ", typeof(T).GetProperties().Where(p => p.Name != "Id").Select(p => $"{p.Name} = @{p.Name}"));
        string sql = $"UPDATE {typeof(T).Name}s SET {updateClause} WHERE Id = @Id";
        return await _dbRepository.ExecuteAsync(sql, obj);
    }
    public async Task<int> DeleteAsync(Guid id)
    {
        string sql = "DELETE FROM " + typeof(T).Name + "s WHERE Id = @Id";
        return await _dbRepository.ExecuteAsync(sql, new { Id = id });
    }
}