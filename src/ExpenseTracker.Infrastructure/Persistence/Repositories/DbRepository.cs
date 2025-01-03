using Dapper;
using MySqlConnector;
using System.Data;
using ExpenseTracker.Application.Common.Interfaces.Persistence;
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;

namespace ExpenseTracker.Infrastructure.Persistence.Repositories;
public class DbRepository : IDbRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DbRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    private IDbConnection CreateConnection() => _connectionFactory.CreateConnection();

    public async Task<int> ExecuteAsync(string sql, object? param = null)
    {
        using (var connection = CreateConnection())
        {
            return await connection.ExecuteAsync(sql, param);
        }
    }

    public async Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null)
    {
        using (var connection = CreateConnection())
        {
            return await connection.QueryFirstOrDefaultAsync<T>(sql, param);
        }
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null)
    {
        using (var connection = CreateConnection())
        {
            return await connection.QueryAsync<T>(sql, param);
        }
    }

    public async Task<T> QuerySingleAsync<T>(string sql, object? param = null)
    {
        using (var connection = CreateConnection())
        {
            return await connection.QuerySingleAsync<T>(sql, param);
        }
    }

    public async Task<IEnumerable<T>> QueryMultipleAsync<T>(string sql, object? param = null)
    {
        using (var connection = CreateConnection())
        {
            return (IEnumerable<T>)await connection.QueryMultipleAsync(sql, param);
        }
    }

    public async Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object? param = null)
    {
        using (var connection = CreateConnection())
        {
            var result = await connection.QuerySingleOrDefaultAsync<T>(sql, param);
            return result != null ? result : default;
        }
    }


    public async Task<int> ExecuteScalarAsync<T>(string sql, object? param = null)
    {
        using (var connection = CreateConnection())
        {
            return await connection.ExecuteScalarAsync<int>(sql, param);
        }
    }

    public async Task<IEnumerable<TResult>> QueryAsync<T1, T2, TResult>(
        string sql,
        Func<T1, T2, TResult> map,
        object? param = null)
    {
        using (var connection = CreateConnection())
        {
            var result = await connection.QueryAsync<T1, T2, TResult>(
                        sql,
                        map,
                        param);
            return result;
        }

    }
}

