using ExpenseTracker.Core.Common.Base;

namespace ExpenseTracker.Core.Entities;

public class InvestmentCategory : Category
{
    public IReadOnlyList<Investment> Investments { get; private set; }
    private InvestmentCategory(Guid id, string name, string description)
        : base(id, name, description)
    {

    }
    public static InvestmentCategory Create(string name, string description)
    {
        return new(Guid.NewGuid(), name, description);
    }
}