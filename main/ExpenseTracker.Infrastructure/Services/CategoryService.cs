using ExpenseTracker.Core.Models;
using ExpenseTracker.Infrastructure.Data.DbSettings;

namespace ExpenseTracker.Infrastructure.Services;

public class CategoryService : BaseService<Category>
{
    public CategoryService(IMongoDbContext context) : base(context, "Category")
    {
    }
}

