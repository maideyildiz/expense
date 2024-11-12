
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.CountryOperations.Common;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Infrastructure.Persistence.Repositories;

public class CountryRepository : BaseRepository<Country>, ICountryRepository
{
    private readonly IDbRepository _dbRepository;
    public CountryRepository(IDbRepository dbRepository)
    : base(dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public async Task<(IEnumerable<GetCountryResult> Items, int TotalCount)> GetCountriesAsync(int page, int pageSize)
    {
        string query = @"
        SELECT c.Id, c.Name
        FROM Countries c
        LIMIT @PageSize OFFSET @Offset";

        var countries = await _dbRepository.QueryAsync<GetCountryResult>(query, new { PageSize = pageSize, Offset = (page - 1) * pageSize });

        string countQuery = "SELECT COUNT(*) FROM Countries";
        int totalCount = await _dbRepository.ExecuteScalarAsync<int>(countQuery);

        return (countries, totalCount);
    }
}
