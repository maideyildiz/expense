using ExpenseTracker.Application.CityOperations.Common;
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Application.Common.Interfaces.Services;

public interface ICityRepository : IBaseRepository<City>
{
    Task<(IEnumerable<GetCityResult> Items, int TotalCount)> GetCitiesByCountryIdAsync(Guid countryId, int page, int pageSize);
    Task<GetCityResult?> GetCityByIdAsync(Guid id);
}