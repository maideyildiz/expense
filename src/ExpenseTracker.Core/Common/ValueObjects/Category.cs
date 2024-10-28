using ExpenseTracker.Core.Common.Models;

namespace ExpenseTracker.Core.Common.ValueObjests;

public sealed class Category : ValueObject
{
    public string Name { get; }

    private Category(string name)
    {
        this.Name = name;
    }

    public static Category Create(string name)
    {
        return new Category(name);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Name;
    }
}
