using ExpenseTracker.Core.Common.Enums;

namespace ExpenseTracker.Core.Common.Base;

public partial class Category : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    protected Category(Guid id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
}