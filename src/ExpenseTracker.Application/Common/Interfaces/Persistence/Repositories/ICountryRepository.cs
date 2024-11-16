using ExpenseTracker.Application.CountryOperations.Common;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;


public interface ICountryRepository : IBaseRepository<Country>
{
    Task<IEnumerable<GetCountryResult>> GetCountriesAsync(int page, int pageSize);
    Task<GetCountryResult?> GetCountryByIdAsync(Guid id);
}