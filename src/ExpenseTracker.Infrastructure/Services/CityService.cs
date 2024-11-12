
using ExpenseTracker.Application.CityOperations.Queries;
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Infrastructure.Services;


public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;

    public CityService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public async Task<(IEnumerable<GetCitiesResult> Items, int TotalCount)> GetCitiesByCountryIdAsync(Guid countryId, int page, int pageSize)
    {
        return await _cityRepository.GetCitiesByCountryIdAsync(countryId, page, pageSize);
    }
}