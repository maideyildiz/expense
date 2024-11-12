using ExpenseTracker.Core.Common.Base;


namespace ExpenseTracker.Core.Entities;

public class Investment : Entity
{
    public Investment()
    {

    }
    public decimal Amount { get; private set; }
    public string Description { get; private set; }
    public Guid UserId { get; private set; }
    public Guid CategoryId { get; private set; }
    private Investment(
        Guid id,
        decimal amount,
        string description,
        Guid userId,
        Guid categoryId)
    {
        Id = id;
        Amount = amount;
        Description = description;
        CategoryId = categoryId;
        UserId = userId;
    }

    public static Investment Create(
        decimal amount,
        string description,
        Guid userId,
        Guid categoryId)
    {
        return new(Guid.NewGuid(), amount, description, userId, categoryId);
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