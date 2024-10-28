using ExpenseTracker.Core.Common.Models;
using ExpenseTracker.Core.UserAggregate.ValueObjects;

namespace ExpenseTracker.Core.UserAggregate.Entities;

public sealed class Subscription : Entity<SubscriptionId>
{
    public string Name { get; }
    public string Description { get; }
    public decimal MonthlyCost { get; }

    public Subscription(SubscriptionId id, string name, string description, decimal monthlyCost) : base(id)
    {
        Name = name;
        Description = description;
        MonthlyCost = monthlyCost;
    }

    public static Subscription Create(string name, string description, decimal monthlyCost)
    {
        return new(SubscriptionId.CreateUnique(), name, description, monthlyCost);
    }
}