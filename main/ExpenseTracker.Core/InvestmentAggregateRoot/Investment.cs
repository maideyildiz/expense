using ExpenseTracker.Core.Common.Models;
using ExpenseTracker.Core.Common.ValueObjests;
using ExpenseTracker.Core.InvestmentAggregateRoot.ValueObjects;

namespace ExpenseTracker.Core.InvestmentAggregateRoot;

public class Investment : AggregateRoot<InvestmentId>
{
    public decimal Amount { get; }
    public DateTime Date { get; }
    public string? Description { get; }
    public Category Category { get; }
    private Investment(InvestmentId id, decimal amount, DateTime date, string? description, Category category) : base(id)
    {
        Amount = amount;
        Date = date;
        Description = description;
        Category = category;
    }

    public static Investment Create(InvestmentId id,
                                    decimal amount,
                                    DateTime date,
                                    string? description,
                                    Category category)
    {
        return new(id, amount, date, description, category);
    }
}