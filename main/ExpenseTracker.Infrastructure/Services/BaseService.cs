using ExpenseTracker.Core.Models;
using ExpenseTracker.Infrastructure.Abstractions;

namespace ExpenseTracker.Infrastructure.Services;

public class BaseService<T> : IBaseService<T> where T : Base
{
    protected readonly IDatabaseConnection _databaseConnection;
    protected readonly string _tableName;

    public BaseService(IDatabaseConnection databaseConnection, string tableName)
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
        string sql = "INSERT INTO " + typeof(T).Name + "s (/* column names */) VALUES (/* values */)";
        return await _databaseConnection.ExecuteAsync(sql, obj);
    }

    public async Task<int> UpdateAsync(T obj)
    {
        string sql = "UPDATE " + typeof(T).Name + "s SET /* column = value */ WHERE Id = @Id";
        return await _databaseConnection.ExecuteAsync(sql, obj);
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        string sql = "DELETE FROM " + typeof(T).Name + "s WHERE Id = @Id";
        return await _databaseConnection.ExecuteAsync(sql, new { Id = id });
    }
}
