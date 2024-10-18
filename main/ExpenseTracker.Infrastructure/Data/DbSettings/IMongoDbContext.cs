using MongoDB.Driver;
namespace ExpenseTracker.Infrastructure.Data.DbSettings;
public interface IMongoDbContext
{
    IMongoCollection<T> GetCollection<T>(string name);
}
