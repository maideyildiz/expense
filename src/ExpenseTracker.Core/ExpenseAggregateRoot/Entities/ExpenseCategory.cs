using ExpenseTracker.Core.Common.Models;
using ExpenseTracker.Core.ExpenseAggregateRoot.ValueObjests;

namespace ExpenseTracker.Core.ExpenseAggregate.Entities;

public class ExpenseCategory : Entity<ExpenseCategoryId>
{
    public string Name { get; }
    public string Description { get; }
    private ExpenseCategory(ExpenseCategoryId id, string name, string description)
        : base(id)
    {
        Name = name;
        Description = description;
    }
    public static ExpenseCategory Create(string name, string description)
    {
        return new(ExpenseCategoryId.CreateUnique(), name, description);
    }
}