using ExpenseTracker.Core.Common.Entities;
using ExpenseTracker.Core.Common.Models;
using ExpenseTracker.Core.Common.ValueObjects;
using ExpenseTracker.Core.ExpenseAggregate.ValueObjests;
using ExpenseTracker.Core.InvestmentAggregate.ValueObjects;
using ExpenseTracker.Core.UserAggregate.Entities;
using ExpenseTracker.Core.UserAggregate.ValueObjects;

namespace ExpenseTracker.Core.UserAggregate;

public class User : AggregateRoot<UserId>
{
    private readonly List<ExpenseId> expenseIds = new();
    private readonly List<InvestmentId> investmentIds = new();
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public decimal MothlySalary { get; private set; }
    public decimal YearlySalary { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public DateTime LastLoginAt { get; private set; }
    public bool IsActive { get; private set; }
    public SubscriptionId SubscriptionId { get; private set; }
    public CityId CityId { get; private set; }
    public IReadOnlyList<ExpenseId> ExpenseIds => this.expenseIds.AsReadOnly();
    public IReadOnlyList<InvestmentId> InvestmentIds => this.investmentIds.AsReadOnly();

    private User(
        UserId id,
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        decimal mothlySalary,
        decimal yearlySalary,
        DateTime createdAt,
        DateTime updatedAt,
        DateTime lastLoginAt,
        bool isActive,
        SubscriptionId subscriptionId,
        CityId cityId)
        : base(id)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.PasswordHash = passwordHash;
        this.MothlySalary = mothlySalary;
        this.YearlySalary = yearlySalary;
        this.CreatedAt = createdAt;
        this.UpdatedAt = updatedAt;
        this.LastLoginAt = lastLoginAt;
        this.IsActive = isActive;
        this.SubscriptionId = subscriptionId;
        this.CityId = cityId;
    }

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        SubscriptionId subscriptionId,
        CityId cityId,
        decimal mothlySalary = decimal.Zero,
        decimal yearlySalary = decimal.Zero)
    {
        return new(UserId.CreateUnique(),
                    firstName,
                    lastName,
                    email,
                    passwordHash,
                    mothlySalary,
                    yearlySalary,
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    true,
                    subscriptionId,
                    cityId);
    }
}
