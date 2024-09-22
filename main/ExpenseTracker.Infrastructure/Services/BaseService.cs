using MongoDB.Driver;
using ExpenseTracker.Infrastructure.Data.DbSettings;
using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Infrastructure.Services;

public class BaseService<T> : IBaseService<T> where T : Base
{
    protected readonly IMongoCollection<T> _collection;

    public BaseService(DbOptions dbOptions, string collectionName)
    {
        var client = new MongoClient(dbOptions.ConnectionString);
        var database = client.GetDatabase(dbOptions.DatabaseName);
        _collection = database.GetCollection<T>(collectionName);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        if (id == Guid.Empty) throw new ArgumentOutOfRangeException(nameof(id), id, "Id must be a valid Guid");

        var filter = Builders<T>.Filter.Eq("Id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task AddAsync(T obj)
    {
        await _collection.InsertOneAsync(obj);
    }

    public async Task UpdateAsync(T obj)
    {
        var filter = Builders<T>.Filter.Eq("Id", obj.Id);
        await _collection.ReplaceOneAsync(filter, obj, new ReplaceOptions { IsUpsert = true });
    }

    public async Task DeleteAsync(Guid id)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        await _collection.DeleteOneAsync(filter);
    }
}

