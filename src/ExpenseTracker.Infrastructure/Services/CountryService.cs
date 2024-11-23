using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.CountryOperations.Common;
using ExpenseTracker.Application.Common.Errors;
using ErrorOr;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using ExpenseTracker.Application.Common.Interfaces.Cache;

namespace ExpenseTracker.Infrastructure.Services;

public class CountryService : ICountryService
{
    private readonly ICacheService _redisCacheService;
    private readonly IDbRepository _dbRepository;

    public CountryService(
        ICacheService redisCacheService,
        IDbRepository dbRepository)
    {
        _redisCacheService = redisCacheService;
        _dbRepository = dbRepository;
    }

    public async Task<(IEnumerable<GetCountryResult> Items, int TotalCount)> GetCountriesAsync(int page, int pageSize)
    {
        var cacheKey = $"GetCountriesAsync_{page}_{pageSize}";
        var cachedData = await _redisCacheService.GetAsync<IEnumerable<GetCountryResult>>(cacheKey);

        if (cachedData != null && cachedData.Any())
        {
            return (cachedData, cachedData.Count());
        }

        string query = @"
        SELECT c.Id, c.Name
        FROM Countries c
        LIMIT @PageSize OFFSET @Offset";

        var data = await _dbRepository.QueryAsync<GetCountryResult>(query, new { PageSize = pageSize, Offset = (page - 1) * pageSize });
        await _redisCacheService.SetAsync(cacheKey, data);
        return (data, data.Count());
    }

    public async Task<ErrorOr<GetCountryResult>> GetCountryByIdAsync(Guid id)
    {
        string cacheKey = $"GetCountryByIdAsync_{id}";
        var cachedData = await _redisCacheService.GetAsync<GetCountryResult>(cacheKey);
        if (cachedData != null)
        {
            return cachedData;
        }
        string sql = $"SELECT Id, Name FROM Countries WHERE Id = @Id";
        var country = await _dbRepository.QueryFirstOrDefaultAsync<GetCountryResult>(sql, new { Id = id });
        if (country == null)
        {
            return Errors.Country.NotFound;
        }
        await _redisCacheService.SetAsync(cacheKey, country);

        return country;
    }
}
