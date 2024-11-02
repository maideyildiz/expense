using ExpenseTracker.Application.Common.Interfaces.Persistence;
using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Core.Common.Entities;

namespace ExpenseTracker.Infrastructure.Persistence.Repositories;
public class CityRepository : BaseRepository<City>, ICityRepository
{
    private new readonly IDbRepository _dbRepository;

    public CityRepository(IDbRepository dbRepository)
        : base(dbRepository)
    {
        _dbRepository = dbRepository;
    }


}