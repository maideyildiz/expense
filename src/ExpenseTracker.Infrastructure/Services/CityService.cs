using ErrorOr;
using ExpenseTracker.Application.Common.Errors;
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Application.CityOperations.Common;
using ExpenseTracker.Application.Common.Interfaces.Cache;

namespace ExpenseTracker.Infrastructure.Services;


public class CityService : ICityService
{
    private readonly ICacheService _redisCacheService;
    private readonly ICityRepository _cityRepository;

    public CityService(
        ICacheService redisCacheService,
        ICityRepository cityRepository)
    {
        _redisCacheService = redisCacheService;
        _cityRepository = cityRepository;
    }

    public async Task<(IEnumerable<GetCityResult> Items, int TotalCount)> GetCitiesByCountryIdAsync(Guid countryId, int page, int pageSize)
    {
        var cacheKey = $"GetCitiesAsync_{page}_{pageSize}";
        var cachedData = await _redisCacheService.GetAsync<IEnumerable<GetCityResult>>(cacheKey);

        if (cachedData != null && cachedData.Any())
        {
            return (cachedData, cachedData.Count());
        }

        var cities = await _cityRepository.GetCitiesByCountryIdAsync(countryId, page, pageSize);
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

        var city = await _cityRepository.GetCityByIdAsync(id);
        if (city == null)
        {
            return Errors.City.NotFound;
        }
        await _redisCacheService.SetAsync(cacheKey, city);

        return city;
    }
}