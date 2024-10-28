using ExpenseTracker.Application.Common.Interfaces.Persistence;

namespace ExpenseTracker.Infrastructure.Persistence;

public class BaseRepository<T> : IBaseRepository<T>
    where T : class
{
    private readonly IDbRepository _dbRepository;
    private readonly string _tableName;

    public BaseRepository(IDbRepository dbRepository, string tableName)
    {
        this._dbRepository = dbRepository;
        this._tableName = tableName;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        string sql = "SELECT * FROM " + typeof(T).Name + "s";
        return await this._dbRepository.QueryAsync<T>(sql);
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        string sql = "SELECT * FROM " + typeof(T).Name + "s WHERE Id = @Id";
        return await this._dbRepository.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
    }

    public async Task<int> AddAsync(T obj)
    {
        string insertClause = string.Join(", ", typeof(T).GetProperties().Select(p => p.Name));
        string valuesClause = string.Join(", ", typeof(T).GetProperties().Select(p => "@" + p.Name));

        string sql = $"INSERT INTO {typeof(T).Name}s ({insertClause}) VALUES ({valuesClause})";
        return await this._dbRepository.ExecuteAsync(sql, obj);
    }

    public async Task<int> UpdateAsync(T obj)
    {
        string updateClause = string.Join(", ", typeof(T).GetProperties().Where(p => p.Name != "Id").Select(p => $"{p.Name} = @{p.Name}"));
        string sql = $"UPDATE {typeof(T).Name}s SET {updateClause} WHERE Id = @Id";
        return await this._dbRepository.ExecuteAsync(sql, obj);
    }


    public async Task<int> DeleteAsync(Guid id)
    {
        string sql = "DELETE FROM " + typeof(T).Name + "s WHERE Id = @Id";
        return await this._dbRepository.ExecuteAsync(sql, new { Id = id });
    }
}
