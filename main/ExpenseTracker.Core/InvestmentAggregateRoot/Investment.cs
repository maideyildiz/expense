using ExpenseTracker.Core.Common.Models;
using ExpenseTracker.Core.Common.ValueObjests;
using ExpenseTracker.Core.InvestmentAggregateRoot.ValueObjects;

namespace ExpenseTracker.Core.InvestmentAggregateRoot;

public class Investment : AggregateRoot<InvestmentId>
{
    public decimal Amount { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    public string Description { get; }
    public Category Category { get; }
    private Investment(InvestmentId id, decimal amount, DateTime createdAt, DateTime updatedAt, string description, Category category) : base(id)
    {
        Amount = amount;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Description = description;
        Category = category;
    }

    public static Investment Create(InvestmentId id,
                                    decimal amount,
                                    DateTime createdAt,
                                    DateTime updatedAt,
                                    string description,
                                    Category category)
    {
        return new(id, amount, createdAt, updatedAt, description, category);
    }
}