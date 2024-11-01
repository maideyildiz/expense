using ExpenseTracker.Core.Common.Models;

namespace ExpenseTracker.Core.InvestmentAggregate.ValueObjects;

public sealed class InvestmentId : ValueObject
{
    public Guid Value { get; private set; }
    private InvestmentId(Guid value)
    {
        this.Value = value;
    }
    public static InvestmentId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value;
    }
}