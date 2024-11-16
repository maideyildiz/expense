using ErrorOr;
using ExpenseTracker.Application.Common.Errors;
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Application.CityOperations.Common;

namespace ExpenseTracker.Infrastructure.Services;


public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;

    public CityService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public async Task<(IEnumerable<GetCityResult> Items, int TotalCount)> GetCitiesByCountryIdAsync(Guid countryId, int page, int pageSize)
    {
        var cities = await _cityRepository.GetCitiesByCountryIdAsync(countryId, page, pageSize);
        return (cities, cities.Count());
    }

    public async Task<ErrorOr<GetCityResult>> GetCityByIdAsync(Guid id)
    {
        var city = await _cityRepository.GetCityByIdAsync(id);
        if (city == null)
        {
            return Errors.City.NotFound;
        }
        GetCityResult getCityResult = new GetCityResult(city.Id, city.Name);
        return getCityResult;
    }
}