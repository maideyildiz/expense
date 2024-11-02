using ExpenseTracker.Core.Common.Models;

namespace ExpenseTracker.Core.UserAggregate.Entities;

public sealed class Subscription : Entity<Guid>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal MonthlyCost { get; private set; }

    private Subscription(Guid id, string name, string description, decimal monthlyCost)
        : base(id)
    {
        this.Name = name;
        this.Description = description;
        this.MonthlyCost = monthlyCost;
    }

    public static Subscription Create(string name, string description, decimal monthlyCost)
    {
        return new(Guid.NewGuid(), name, description, monthlyCost);
    }
}