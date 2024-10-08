using ExpenseTracker.Core.Models;
using ExpenseTracker.Infrastructure.Data.DbSettings;

namespace ExpenseTracker.Infrastructure.Services;

public class ExpenseService : BaseService<Expense>
{
    public ExpenseService(DbOptions dbOptions) : base(dbOptions, "Expense")
    {
    }

}

