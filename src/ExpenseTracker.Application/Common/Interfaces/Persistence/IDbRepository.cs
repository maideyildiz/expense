namespace ExpenseTracker.Application.Common.Interfaces.Persistence;
public interface IDbRepository
{
    Task<int> ExecuteAsync(string sql, object? param = null);
    Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null);
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null);
    Task<T> QuerySingleAsync<T>(string sql, object? param = null);
    Task<IEnumerable<T>> QueryMultipleAsync<T>(string sql, object? param = null);
    Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object? param = null);
    Task<int> ExecuteScalarAsync<T>(string sql, object? param = null);
}
