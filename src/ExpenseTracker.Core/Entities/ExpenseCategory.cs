using ExpenseTracker.Core.Common.Base;

namespace ExpenseTracker.Core.Entities;

public class ExpenseCategory : Category
{
    public IReadOnlyList<Expense> Expenses { get; private set; }
    private ExpenseCategory(Guid id, string name, string description)
        : base(id, name, description)
    {

    }
    public static ExpenseCategory Create(string name, string description)
    {
        return new(Guid.NewGuid(), name, description);
    }
}