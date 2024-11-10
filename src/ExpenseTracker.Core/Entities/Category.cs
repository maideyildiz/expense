using ExpenseTracker.Core.Common.Enums;
using ExpenseTracker.Core.Common.Base;

namespace ExpenseTracker.Core.Entities;

public class Category : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public CategoryType CategoryType { get; private set; }
    private Category(Guid id, string name, string description, CategoryType categoryType)
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