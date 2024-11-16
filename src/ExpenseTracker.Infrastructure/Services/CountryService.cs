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
    private readonly IDistributedCache _cache;
    private readonly ICountryRepository _countryRepository;

    public CountryService(
        ICountryRepository countryRepository,
        IDistributedCache cache)
    {
        _countryRepository = countryRepository;
        _cache = cache;
    }

    public async Task<(IEnumerable<GetCountryResult> Items, int TotalCount)> GetCountriesAsync(int page, int pageSize)
    {
        string key = $"GetCountriesAsync_{page}_{pageSize}";

        var cachedData = await _cache.GetStringAsync(key);
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedResult = JsonSerializer.Deserialize<IEnumerable<GetCountryResult>>(cachedData);
            if (cachedResult != null && cachedResult.Any())
            {
                return (cachedResult, cachedResult.Count());
            }
        }

        var data = await _countryRepository.GetCountriesAsync(page, pageSize);
        var serializedData = JsonSerializer.Serialize(data);
        await _cache.SetStringAsync(key, serializedData, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
        });
        return (data, data.Count());
    }

    public async Task<ErrorOr<GetCountryResult>> GetCountryByIdAsync(Guid id)
    {
        string key = $"GetCountryByIdAsync{id}";

        var cachedData = await _cache.GetStringAsync(key);
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedResult = JsonSerializer.Deserialize<GetCountryResult>(cachedData);
            if (cachedResult != null)
            {
                return cachedResult;
            }
        }

        var country = await _countryRepository.GetCountryByIdAsync(id);
        if (country == null)
        {
            return Errors.Country.NotFound;
        }
        var serializedData = JsonSerializer.Serialize(country);
        await _cache.SetStringAsync(key, serializedData, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
        });
        return country;
    }
}
