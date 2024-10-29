using ExpenseTracker.Core.Common.Models;

namespace InvestmentTracker.Core.InvestmentAggregateRoot.ValueObjests;

public sealed class InvestmentCategoryId : ValueObject
{
    public Guid Value { get; }
    private InvestmentCategoryId(Guid value)
    {
        Value = value;
    }
    public static InvestmentCategoryId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}