using ExpenseTracker.Core.Common.Models;

namespace ExpenseTracker.Core.ExpenseAggregateRoot.ValueObjests;

public sealed class ExpenseCategoryId : ValueObject
{
    public Guid Value { get; }
    private ExpenseCategoryId(Guid value)
    {
        Value = value;
    }
    public static ExpenseCategoryId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}