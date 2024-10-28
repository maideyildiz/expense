using ExpenseTracker.Core.Common.Models;

namespace ExpenseTracker.Core.UserAggregateRoot.ValueObjects;

public sealed class SubscriptionId : ValueObject
{
    public Guid Value { get; }
    private SubscriptionId(Guid value)
    {
        Value = value;
    }
    public static SubscriptionId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}