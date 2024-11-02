using ExpenseTracker.Core.Common.Models;
namespace ExpenseTracker.Core.ExpenseAggregate;

public class Expense : AggregateRoot<Guid>
{
    public decimal Amount { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public string Description { get; private set; }
    public Guid CategoryId { get; private set; }
    public Guid UserId { get; private set; }

    private Expense(
        Guid id,
        decimal amount,
        DateTime createdAt,
        DateTime updatedAt,
        string description,
        Guid categoryId,
        Guid userId)
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
        decimal amount,
        DateTime createdAt,
        DateTime updatedAt,
        string description,
        Guid categoryId,
        Guid userId)
    {
        return new(
            Guid.NewGuid(),
            amount,
            createdAt,
            updatedAt,
            description,
            categoryId,
            userId);
    }
}