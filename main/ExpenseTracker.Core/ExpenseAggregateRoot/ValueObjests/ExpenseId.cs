using ExpenseTracker.Core.Common.Models;

namespace ExpenseTracker.Core.ExpenseAggregateRoot.ValueObjests;

public sealed class ExpenseId : ValueObject
{
    public Guid Value { get; }
    private ExpenseId(Guid value)
    {
        Value = value;
    }
    public static ExpenseId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}