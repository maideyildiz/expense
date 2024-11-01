using ExpenseTracker.Core.Common.Models;

namespace ExpenseTracker.Core.ExpenseAggregate.ValueObjests;

public sealed class ExpenseId : ValueObject
{
    public Guid Value { get; private set; }
    private ExpenseId(Guid value)
    {
        this.Value = value;
    }
    public static ExpenseId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value;
    }
}