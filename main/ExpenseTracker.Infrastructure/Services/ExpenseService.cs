using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Infrastructure.Services;

public class ExpenseService : BaseService<Expense>
{
    public ExpenseService() : base("expenses")
    {
    }
}

