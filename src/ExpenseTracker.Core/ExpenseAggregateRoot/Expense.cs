using ExpenseTracker.Core.Common.Models;
using ExpenseTracker.Core.Common.ValueObjects;
using ExpenseTracker.Core.ExpenseAggregate.ValueObjests;
using ExpenseTracker.Core.UserAggregate.ValueObjects;
namespace ExpenseTracker.Core.ExpenseAggregate;

public class Expense : AggregateRoot<ExpenseId>
{
    public decimal Amount { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    public string Description { get; }
    public CategoryId CategoryId { get; }
    public UserId UserId { get; }

    private Expense(
        ExpenseId id,
        decimal amount,
        DateTime createdAt,
        DateTime updatedAt,
        string description,
        CategoryId categoryId,
        UserId userId)
        : base(id)
    {
        this.Amount = amount;
        this.CreatedAt = createdAt;
        this.UpdatedAt = updatedAt;
        this.Description = description;
        this.CategoryId = categoryId;
        this.UserId = userId;
    }

    public static Expense Create(
        ExpenseId id,
        decimal amount,
        DateTime createdAt,
        DateTime updatedAt,
        string description,
        CategoryId categoryId,
        UserId userId)
    {
        return new(
            id,
            amount,
            createdAt,
            updatedAt,
            description,
            categoryId,
            userId);
    }
}