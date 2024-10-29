

using ExpenseTracker.Core.Common.Enums;
using ExpenseTracker.Core.Common.Models;
using ExpenseTracker.Core.Common.ValueObjects;

namespace ExpenseTracker.Core.Common.Entities;

public class Category : Entity<CategoryId>
{
    public string Name { get; }
    public string Description { get; }

    public CategoryType CategoryType { get; }
    private Category(CategoryId id, string name, string description, CategoryType categoryType)
        : base(id)
    {
        Name = name;
        Description = description;
        CategoryType = categoryType;
    }
    public static Category Create(string name, string description, CategoryType categoryType)
    {
        return new(CategoryId.CreateUnique(), name, description, categoryType);
    }
}