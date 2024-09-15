using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Infrastructure.Services;

    public class BaseService<T> : IBaseService<T> where T : Base
    {
        private readonly IMongoCollection<T> _collection;
        private readonly string _databaseName="";
        private readonly string _connectionString="";

        public BaseService(string collectionName)
        {
            var client = new MongoClient(_connectionString);
            var database = client.GetDatabase(_databaseName);
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id), id, "Id must be a positive integer");

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

        public async Task DeleteAsync(int id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            await _collection.DeleteOneAsync(filter);
        }
    }

