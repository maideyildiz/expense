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
}