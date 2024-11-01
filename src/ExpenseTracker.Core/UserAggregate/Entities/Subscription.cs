using ExpenseTracker.Core.Common.Models;
using ExpenseTracker.Core.UserAggregate.ValueObjects;

namespace ExpenseTracker.Core.UserAggregate.Entities;

public sealed class Subscription : Entity<SubscriptionId>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal MonthlyCost { get; private set; }

    private Subscription(SubscriptionId id, string name, string description, decimal monthlyCost)
        : base(id)
    {
        this.Name = name;
        this.Description = description;
        this.MonthlyCost = monthlyCost;
    }

    public static Subscription Create(string name, string description, decimal monthlyCost)
    {
        return new(SubscriptionId.CreateUnique(), name, description, monthlyCost);
    }
}