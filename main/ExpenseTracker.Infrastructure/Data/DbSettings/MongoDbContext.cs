using MongoDB.Driver;

namespace ExpenseTracker.Infrastructure.Data.DbSettings;
public class MongoDbContext : IMongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(DbOptions dbOptions)
    {
        var client = new MongoClient(dbOptions.ConnectionString);
        _database = client.GetDatabase(dbOptions.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }
}
