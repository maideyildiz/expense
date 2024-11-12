using ErrorOr;

using ExpenseTracker.Application.CountryOperations.Common;

namespace ExpenseTracker.Application.Common.Interfaces.Services;


public interface ICountryService
{
    Task<(IEnumerable<GetCountryResult> Items, int TotalCount)> GetCountriesAsync(int page, int pageSize);

    Task<ErrorOr<GetCountryResult>> GetCountryByIdAsync(Guid id);
}