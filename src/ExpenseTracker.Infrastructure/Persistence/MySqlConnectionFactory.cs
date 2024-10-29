using System.Data;

using ExpenseTracker.Application.Common.Interfaces.Persistence;

using MySqlConnector;

namespace ExpenseTracker.Infrastructure.Persistence;

public class MySqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public MySqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    IDbConnection IDbConnectionFactory.CreateConnection()
    {
        var connection = new MySqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
}