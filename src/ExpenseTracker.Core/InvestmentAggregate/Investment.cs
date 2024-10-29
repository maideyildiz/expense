using ExpenseTracker.Core.Common.Entities;
using ExpenseTracker.Core.Common.Models;
using ExpenseTracker.Core.Common.ValueObjects;
using ExpenseTracker.Core.InvestmentAggregate.ValueObjects;
using ExpenseTracker.Core.UserAggregate.ValueObjects;


namespace ExpenseTracker.Core.InvestmentAggregate;

public class Investment : AggregateRoot<InvestmentId>
{
    public decimal Amount { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    public string Description { get; }
    public UserId UserId { get; }
    public CategoryId CategoryId { get; }
    private Investment(
        InvestmentId id,
        decimal amount,
        DateTime createdAt,
        DateTime updatedAt,
        string description,
        UserId userId,
        CategoryId categoryId)
        : base(id)
    {
        this.Amount = amount;
        this.CreatedAt = createdAt;
        this.UpdatedAt = updatedAt;
        this.Description = description;
        this.CategoryId = categoryId;
        this.UserId = userId;
    }

    public static Investment Create(
        InvestmentId id,
        decimal amount,
        DateTime createdAt,
        DateTime updatedAt,
        string description,
        UserId userId,
        CategoryId categoryId)
    {
        return new(id, amount, createdAt, updatedAt, description, userId, categoryId);
    }
}