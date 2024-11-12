using ErrorOr;

using ExpenseTracker.Application.CityOperations.Common;

namespace ExpenseTracker.Application.Common.Interfaces.Services;

public interface ICityService
{
    Task<(IEnumerable<GetCityResult> Items, int TotalCount)> GetCitiesByCountryIdAsync(Guid countryId, int page, int pageSize);
    Task<ErrorOr<GetCityResult>> GetCityByIdAsync(Guid id);
}