using ExpenseTracker.Application.Common.Interfaces.Persistence;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Infrastructure.Abstractions;

namespace ExpenseTracker.Infrastructure.Persistence;

public class BaseRepository<T> : IBaseRepository<T> where T : Base
{
    protected readonly IDatabaseConnection _databaseConnection;
    protected readonly string _tableName;

    public BaseRepository(IDatabaseConnection databaseConnection, string tableName)
    {
        _databaseConnection = databaseConnection;
        _tableName = tableName;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        string sql = "SELECT * FROM " + typeof(T).Name + "s";
        return await _databaseConnection.QueryAsync<T>(sql);
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        string sql = "SELECT * FROM " + typeof(T).Name + "s WHERE Id = @Id";
        return await _databaseConnection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
    }

    public async Task<int> AddAsync(T obj)
    {
        var (columnNames, parameterNames, _) = Base.GetInsertAndUpdateColumns<T>();
        string sql = $"INSERT INTO {typeof(T).Name}s ({columnNames}) VALUES ({parameterNames})";
        return await _databaseConnection.ExecuteAsync(sql, obj);
    }

    public async Task<int> UpdateAsync(T obj)
    {
        var (_, _, setClause) = Base.GetInsertAndUpdateColumns<T>();
        string sql = $"UPDATE {typeof(T).Name}s SET {setClause} WHERE Id = @Id";
        return await _databaseConnection.ExecuteAsync(sql, obj);
    }


    public async Task<int> DeleteAsync(Guid id)
    {
        string sql = "DELETE FROM " + typeof(T).Name + "s WHERE Id = @Id";
        return await _databaseConnection.ExecuteAsync(sql, new { Id = id });
    }
}
