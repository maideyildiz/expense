using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Infrastructure.Services;

public class CategoryService : BaseService<Category>
{
    public CategoryService() : base("Category")
    {
    }
}

