using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.CountryOperations.Common;
using ExpenseTracker.Application.Common.Errors;
using ErrorOr;

namespace ExpenseTracker.Infrastructure.Services;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _countryRepository;

    public CountryService(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<(IEnumerable<GetCountryResult> Items, int TotalCount)> GetCountriesAsync(int page, int pageSize)
    {
        return await _countryRepository.GetCountriesAsync(page, pageSize);
    }

    public async Task<ErrorOr<GetCountryResult>> GetCountryByIdAsync(Guid id)
    {
        var country = await _countryRepository.GetCountryByIdAsync(id);
        if (country == null)
        {
            return Errors.Country.NotFound;
        }
        GetCountryResult getCountryResult = new GetCountryResult(country.Id, country.Name);
        return getCountryResult;
    }
}
