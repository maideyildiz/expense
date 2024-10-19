using ExpenseTracker.Core.Models;
using ExpenseTracker.Infrastructure.Abstractions;

namespace ExpenseTracker.Infrastructure.Services;

public class CategoryService : BaseService<Category>
{
    public CategoryService(IDatabaseConnection dbConnection) : base(dbConnection, "Category")
    {
    }
}


