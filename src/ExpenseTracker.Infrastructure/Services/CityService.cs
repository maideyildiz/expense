using ErrorOr;
using ExpenseTracker.Application.Common.Errors;
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Application.CityOperations.Common;
using ExpenseTracker.Application.Common.Interfaces.Cache;
using ExpenseTracker.Application.CountryOperations.Common;

namespace ExpenseTracker.Infrastructure.Services;


public class CityService : ICityService
{
    private readonly ICacheService _redisCacheService;
    private readonly IDbRepository _dbRepository;

    public CityService(
        ICacheService redisCacheService,
        IDbRepository dbRepository)
    {
        _redisCacheService = redisCacheService;
        _dbRepository = dbRepository;
    }

    public async Task<(IEnumerable<GetCityResult> Items, int TotalCount)> GetCitiesByCountryIdAsync(Guid countryId, int page, int pageSize)
    {
        var cacheKey = $"GetCitiesAsync_{page}_{pageSize}";
        var cachedData = await _redisCacheService.GetAsync<IEnumerable<GetCityResult>>(cacheKey);
        if (cachedData != null && cachedData.Any())
        {
            return (cachedData, cachedData.Count());
        }

        string query = @"
        SELECT c.Id, c.Name
        FROM Cities c
        LEFT JOIN Countries co ON c.CountryId = co.Id
        WHERE co.Id = @CountryId
        LIMIT @PageSize OFFSET @Offset";

        var cities = await _dbRepository.QueryAsync<GetCityResult>(query, new { CountryId = countryId, PageSize = pageSize, Offset = (page - 1) * pageSize });
        await _redisCacheService.SetAsync(cacheKey, cities);
        return (cities, cities.Count());
    }

    public async Task<ErrorOr<GetCityResult>> GetCityByIdAsync(Guid id)
    {
        string cacheKey = $"GetCityByIdAsync{id}";
        var cachedData = await _redisCacheService.GetAsync<GetCityResult>(cacheKey);
        if (cachedData != null)
        {
            return cachedData;
        }
        string sql = $"SELECT Id, Name FROM Cities WHERE Id = @Id";
        var city = await _dbRepository.QueryFirstOrDefaultAsync<GetCityResult>(sql, new { Id = id });
        if (city == null)
        {
            return Errors.City.NotFound;
        }
        await _redisCacheService.SetAsync(cacheKey, city);

        return city;
    }
}