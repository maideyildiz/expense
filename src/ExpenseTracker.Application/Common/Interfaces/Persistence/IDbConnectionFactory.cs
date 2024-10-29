using System.Data;

namespace ExpenseTracker.Application.Common.Interfaces.Persistence;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}