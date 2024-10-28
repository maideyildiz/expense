using ExpenseTracker.Core.Common.Models;
using ExpenseTracker.Core.Common.ValueObjests;
using ExpenseTracker.Core.ExpenseAggregateRoot.ValueObjests;

namespace ExpenseTracker.Core.ExpenseAggregateRoot;

public class Expense : AggregateRoot<ExpenseId>
{
    public decimal Amount { get; }
    public DateTime Date { get; }
    public string Description { get; }
    public Category Category { get; }

    private Expense(ExpenseId id, decimal amount, DateTime date, string description, Category category) : base(id)
    {
        Amount = amount;
        Date = date;
        Description = description;
        Category = category;
    }

    public static Expense Create(ExpenseId id,
                                 decimal amount,
                                 DateTime date,
                                 string description,
                                 Category category)
    {
        return new(id, amount, date, description, category);
    }
}