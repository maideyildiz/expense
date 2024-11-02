using ExpenseTracker.Core.Common.Enums;
using ExpenseTracker.Core.Common.Models;

namespace ExpenseTracker.Core.Common.Entities;

public class Category : Entity<Guid>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public CategoryType CategoryType { get; private set; }
    private Category(Guid id, string name, string description, CategoryType categoryType)
        : base(id)
    {
        Name = name;
        Description = description;
        CategoryType = categoryType;
    }
    public static Category Create(string name, string description, CategoryType categoryType)
    {
        return new(Guid.NewGuid(), name, description, categoryType);
    }
}