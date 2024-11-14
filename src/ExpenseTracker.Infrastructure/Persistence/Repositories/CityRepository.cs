
using ExpenseTracker.Application.CityOperations.Common;
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Infrastructure.Persistence.Repositories;


public class CityRepository : BaseRepository<City>, ICityRepository
{
    private readonly IDbRepository _dbRepository;
    public CityRepository(IDbRepository dbRepository)
    : base(dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public async Task<(IEnumerable<GetCityResult> Items, int TotalCount)> GetCitiesByCountryIdAsync(Guid countryId, int page, int pageSize)
    {
        string query = @"
        SELECT c.Id, c.Name
        FROM Cities c
        LEFT JOIN Countries co ON c.CountryId = co.Id
        WHERE co.Id = @CountryId
        LIMIT @PageSize OFFSET @Offset";

        var cities = await _dbRepository.QueryAsync<GetCityResult>(query, new { CountryId = countryId, PageSize = pageSize, Offset = (page - 1) * pageSize });

        string countQuery = "SELECT COUNT(*) FROM Cities WHERE CountryId = @CountryId";
        int totalCount = await _dbRepository.ExecuteScalarAsync<int>(countQuery, new { CountryId = countryId });

        return (cities, totalCount);
    }

    public async Task<GetCityResult?> GetCityByIdAsync(Guid id)
    {
        var query = @"
            SELECT c.Id, c.Name
            FROM Cities c
            WHERE c.Id = @Id";

        return await _dbRepository.QuerySingleOrDefaultAsync<GetCityResult>(query, new { Id = id });
    }
}