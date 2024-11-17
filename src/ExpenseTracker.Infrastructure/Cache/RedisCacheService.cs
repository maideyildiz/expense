using ExpenseTracker.Application.Common.Interfaces.Cache;

using Newtonsoft.Json;

using StackExchange.Redis;

namespace ExpenseTracker.Infrastructure.Cache;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _database;

    public RedisCacheService(
        IConnectionMultiplexer redisConnection)
    {
        _database = redisConnection.GetDatabase();
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var data = await _database.StringGetAsync(key);
        return data.HasValue ? JsonConvert.DeserializeObject<T>(data) : default;
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        var jsonData = JsonConvert.SerializeObject(value);
        await _database.StringSetAsync(key, jsonData, expiration.HasValue ? expiration.Value : TimeSpan.FromHours(1));
    }

    public async Task RemoveAsync(string key)
    {
        await _database.KeyDeleteAsync(key);
    }
}