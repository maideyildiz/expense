// using MongoDB.Driver;
// using System.Collections.Generic;
// using System.Threading.Tasks;

// namespace ExpenseTracker.Tests.Infrastructure.Test;

// public class TestConnectionService<T> where T : class
// {
//     private readonly IMongoCollection<T> _collection;

//     public TestConnectionService(string collectionName)
//     {
//         var client = new MongoClient("mongodb+srv://maideyildizz:qoHUa4iSqms2GvWH@expensetrackertestdb.v9x2c.mongodb.net/");
//         var database = client.GetDatabase("ExpenseTrackerTestDb");
//         _collection = database.GetCollection<T>(collectionName);
//     }

//     public async Task<List<T>> GetAllAsync()
//     {
//         return await _collection.Find(_ => true).ToListAsync();
//     }

//     public async Task<T?> GetByIdAsync(string id)
//     {
//         return await _collection.Find(Builders<T>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
//     }

//     public async Task AddAsync(T obj)
//     {
//         await _collection.InsertOneAsync(obj);
//     }

//     public async Task UpdateAsync(string id, T obj)
//     {
//         await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", id), obj);
//     }

//     public async Task DeleteAsync(string id)
//     {
//         await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
//     }
// }

