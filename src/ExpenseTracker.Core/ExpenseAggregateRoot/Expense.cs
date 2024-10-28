namespace ExpenseTracker.Core.ExpenseAggregate;
using ExpenseTracker.Core.Common.Models;
using ExpenseTracker.Core.Common.ValueObjests;
using ExpenseTracker.Core.ExpenseAggregate.ValueObjests;
public class Expense : AggregateRoot<ExpenseId>
{
    public decimal Amount { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    public string Description { get; }
    public Category Category { get; }

    private Expense(ExpenseId id, decimal amount, DateTime createdAt, DateTime updatedAt, string description, Category category)
        : base(id)
    {
        this.Amount = amount;
        this.CreatedAt = createdAt;
        this.UpdatedAt = updatedAt;
        this.Description = description;
        this.Category = category;
    }

    public static Expense Create(
        ExpenseId id,
        decimal amount,
        DateTime createdAt,
        DateTime updatedAt,
        string description,
        Category category)
    {
        return new(id, amount, createdAt, updatedAt, description, category);
    }
}