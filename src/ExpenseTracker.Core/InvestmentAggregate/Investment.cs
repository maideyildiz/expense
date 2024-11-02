using ExpenseTracker.Core.Common.Entities;
using ExpenseTracker.Core.Common.Models;


namespace ExpenseTracker.Core.InvestmentAggregate;

public class Investment : AggregateRoot<Guid>
{
    public decimal Amount { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public string Description { get; private set; }
    public Guid UserId { get; private set; }
    public Guid CategoryId { get; private set; }
    private Investment(
        Guid id,
        decimal amount,
        DateTime createdAt,
        DateTime updatedAt,
        string description,
        Guid userId,
        Guid categoryId)
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
        decimal amount,
        DateTime createdAt,
        DateTime updatedAt,
        string description,
        Guid userId,
        Guid categoryId)
    {
        return new(Guid.NewGuid(), amount, createdAt, updatedAt, description, userId, categoryId);
    }
}