using ExpenseTracker.Core.Models;
using ExpenseTracker.Infrastructure.Abstractions;

namespace ExpenseTracker.Infrastructure.Services;

public class ExpenseService : BaseService<Expense>
{
    public ExpenseService(IDatabaseConnection dbConnection) : base(dbConnection, "Expense")
    {
    }

}

