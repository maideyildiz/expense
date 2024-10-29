using InvestmentTracker.Core.InvestmentAggregateRoot.ValueObjests;
using ExpenseTracker.Core.Common.Models;

namespace ExpenseTracker.Core.InvestmentAggregate.Entities;

public class InvestmentCategory : Entity<InvestmentCategoryId>
{
    public string Name { get; }
    public string Description { get; }
    private InvestmentCategory(InvestmentCategoryId id, string name, string description)
        : base(id)
    {
        Name = name;
        Description = description;
    }
    public static InvestmentCategory Create(string name, string description)
    {
        return new(InvestmentCategoryId.CreateUnique(), name, description);
    }
}