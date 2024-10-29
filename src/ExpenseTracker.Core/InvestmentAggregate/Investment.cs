using ExpenseTracker.Core.Common.Entities;
using ExpenseTracker.Core.Common.Models;
using ExpenseTracker.Core.ExpenseAggregateRoot.ValueObjests;
using ExpenseTracker.Core.InvestmentAggregate.ValueObjects;
using ExpenseTracker.Core.UserAggregate.ValueObjects;

using InvestmentTracker.Core.InvestmentAggregateRoot.ValueObjests;

namespace ExpenseTracker.Core.InvestmentAggregate;

public class Investment : AggregateRoot<InvestmentId>
{
    public decimal Amount { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    public string Description { get; }
    public UserId UserId { get; }
    public InvestmentCategoryId InvestmentCategoryId { get; }
    private Investment(
        InvestmentId id,
        decimal amount,
        DateTime createdAt,
        DateTime updatedAt,
        string description,
        UserId userId,
        InvestmentCategoryId investmentCategoryId)
        : base(id)
    {
        this.Amount = amount;
        this.CreatedAt = createdAt;
        this.UpdatedAt = updatedAt;
        this.Description = description;
        this.InvestmentCategoryId = investmentCategoryId;
        this.UserId = userId;
    }

    public static Investment Create(
        InvestmentId id,
        decimal amount,
        DateTime createdAt,
        DateTime updatedAt,
        string description,
        UserId userId,
        InvestmentCategoryId investmentCategoryId)
    {
        return new(id, amount, createdAt, updatedAt, description, userId, investmentCategoryId);
    }
}