using ExpenseTracker.Core.Common.Models;

namespace ExpenseTracker.Core.Common.ValueObjects;

public sealed class CountryId : ValueObject
{
    public Guid Value { get; }
    private CountryId(Guid value)
    {
        Value = value;
    }
    public static CountryId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}