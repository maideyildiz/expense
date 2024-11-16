using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.CountryOperations.Common;
using ExpenseTracker.Application.Common.Errors;
using ErrorOr;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace ExpenseTracker.Infrastructure.Services;

public class CountryService : ICountryService
{
    private readonly ICacheService _redisCacheService;
    private readonly ICountryRepository _countryRepository;

    public CountryService(
        ICacheService redisCacheService,
        ICountryRepository countryRepository)
    {
        _redisCacheService = redisCacheService;
        _countryRepository = countryRepository;
    }

    public async Task<(IEnumerable<GetCountryResult> Items, int TotalCount)> GetCountriesAsync(int page, int pageSize)
    {
        var cacheKey = $"GetCountriesAsync_{page}_{pageSize}";
        var cachedData = await _redisCacheService.GetAsync<IEnumerable<GetCountryResult>>(cacheKey);

        if (cachedData != null && cachedData.Any())
        {
            return (cachedData, cachedData.Count());
        }

        var data = await _countryRepository.GetCountriesAsync(page, pageSize);
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

        var country = await _countryRepository.GetCountryByIdAsync(id);
        if (country == null)
        {
            return Errors.Country.NotFound;
        }
        await _redisCacheService.SetAsync(cacheKey, country);

        return country;
    }
}
