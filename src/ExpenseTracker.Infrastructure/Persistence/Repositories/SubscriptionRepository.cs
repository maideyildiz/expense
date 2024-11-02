using ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;
using ExpenseTracker.Core.UserAggregate.Entities;

namespace ExpenseTracker.Infrastructure.Persistence.Repositories;

public class SubscriptionRepository : BaseRepository<Subscription>, ISubscriptionRepository
{
    private new readonly IDbRepository _dbRepository;

    public SubscriptionRepository(IDbRepository dbRepository)
        : base(dbRepository)
    {
        _dbRepository = dbRepository;
    }


}