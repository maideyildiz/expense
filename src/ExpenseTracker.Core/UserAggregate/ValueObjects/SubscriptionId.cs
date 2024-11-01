using ExpenseTracker.Core.Common.Models;

namespace ExpenseTracker.Core.UserAggregate.ValueObjects;

public sealed class SubscriptionId : ValueObject
{
    public Guid Value { get; private set; }
    private SubscriptionId(Guid value)
    {
        this.Value = value;
    }
    public static SubscriptionId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value;
    }
}