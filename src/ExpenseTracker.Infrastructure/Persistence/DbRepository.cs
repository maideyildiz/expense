using System.Data;
using Dapper;
using ExpenseTracker.Application.Common.Interfaces.Persistence;
using MySqlConnector;

namespace ExpenseTracker.Infrastructure.Persistence;

public class DbRepository : IDbRepository
{
    private readonly string connectionString;

    public DbRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    private IDbConnection CreateConnection()
    {
        var connection = new MySqlConnection(this.connectionString);
        connection.Open();
        return connection;
    }

    public async Task<int> ExecuteAsync(string sql, object? param = null)
    {
        using (var connection = this.CreateConnection())
        {
            return await connection.ExecuteAsync(sql, param);
        }
    }

    public async Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null)
    {
        using (var connection = this.CreateConnection())
        {
            return await connection.QueryFirstOrDefaultAsync<T>(sql, param);
        }
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null)
    {
        using (var connection = this.CreateConnection())
        {
            return await connection.QueryAsync<T>(sql, param);
        }
    }

    public async Task<T> QuerySingleAsync<T>(string sql, object? param = null)
    {
        using (var connection = this.CreateConnection())
        {
            return await connection.QuerySingleAsync<T>(sql, param);
        }
    }

    public async Task<IEnumerable<T>> QueryMultipleAsync<T>(string sql, object? param = null)
    {
        using (var connection = this.CreateConnection())
        {
            return (IEnumerable<T>)await connection.QueryMultipleAsync(sql, param);
        }
    }

    public async Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object? param = null)
    {
        using (var connection = this.CreateConnection())
        {
            var result = await connection.QuerySingleOrDefaultAsync<T>(sql, param);
            return result != null ? result : default;
        }
    }


    public async Task<int> ExecuteScalarAsync<T>(string sql, object? param = null)
    {
        using (var connection = this.CreateConnection())
        {
            return await connection.ExecuteScalarAsync<int>(sql, param);
        }
    }
}

