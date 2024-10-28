using ExpenseTracker.Application.Common.Interfaces.Persistence;

namespace ExpenseTracker.Infrastructure.Persistence;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly IDbRepository _dbRepository;
    protected readonly string _tableName;

    public BaseRepository(IDbRepository dbRepository, string tableName)
    {
        _dbRepository = dbRepository;
        _tableName = tableName;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        string sql = "SELECT * FROM " + typeof(T).Name + "s";
        return await _dbRepository.QueryAsync<T>(sql);
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        string sql = "SELECT * FROM " + typeof(T).Name + "s WHERE Id = @Id";
        return await _dbRepository.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
    }

    // public async Task<int> AddAsync(T obj)
    // {
    //     var (columnNames, parameterNames, _) = Base.GetInsertAndUpdateColumns<T>();
    //     string sql = $"INSERT INTO {typeof(T).Name}s ({columnNames}) VALUES ({parameterNames})";
    //     return await _dbRepository.ExecuteAsync(sql, obj);
    // }

    // public async Task<int> UpdateAsync(T obj)
    // {
    //     var (_, _, setClause) = Base.GetInsertAndUpdateColumns<T>();
    //     string sql = $"UPDATE {typeof(T).Name}s SET {setClause} WHERE Id = @Id";
    //     return await _dbRepository.ExecuteAsync(sql, obj);
    // }


    public async Task<int> DeleteAsync(Guid id)
    {
        string sql = "DELETE FROM " + typeof(T).Name + "s WHERE Id = @Id";
        return await _dbRepository.ExecuteAsync(sql, new { Id = id });
    }
}
