using ExpenseTracker.Application.CityOperations.Queries;

namespace ExpenseTracker.Application.Common.Interfaces.Services;

public interface ICityService
{
    Task<(IEnumerable<GetCitiesResult> Items, int TotalCount)> GetCitiesByCountryIdAsync(Guid countryId, int page, int pageSize);
}