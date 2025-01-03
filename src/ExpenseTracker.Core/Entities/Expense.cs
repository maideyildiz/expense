using ExpenseTracker.Core.Common.Base;
namespace ExpenseTracker.Core.Entities;

public class Expense : Entity
{
    public Expense()
    {

    }
    public decimal Amount { get; private set; }
    public string Description { get; private set; }
    public Guid CategoryId { get; private set; }
    public Guid UserId { get; private set; }

    private Expense(
        Guid id,
        decimal amount,
        string description,
        Guid categoryId,
        Guid userId)
    {
        Id = id;
        Amount = amount;
        Description = description;
        CategoryId = categoryId;
        UserId = userId;
    }

    public static Expense Create(
        decimal amount,
        string description,
        Guid categoryId,
        Guid userId)
    {
        return new(
            Guid.NewGuid(),
            amount,
            description,
            categoryId,
            userId);
    }

    public void Update(
        decimal? amount,
        string? description,
        Guid? categoryId)
    {
        Amount = amount is not null ? amount.Value : Amount;
        Description = description is not null ? description : Description;
        CategoryId = categoryId is not null ? categoryId.Value : CategoryId;
        UpdatedAt = DateTime.UtcNow;
    }
}