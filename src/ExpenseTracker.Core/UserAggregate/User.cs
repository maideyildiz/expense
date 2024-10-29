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
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string PasswordHash { get; }
    public decimal MothlySalary { get; }
    public decimal YearlySalary { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    public DateTime LastLoginAt { get; }
    public bool IsActive { get; }
    public SubscriptionId SubscriptionId { get; }
    public CityId CityId { get; }
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
        decimal mothlySalary,
        decimal yearlySalary,
        DateTime createdAt,
        DateTime updatedAt,
        DateTime lastLoginAt,
        bool isActive,
        SubscriptionId subscriptionId,
        CityId cityId)
    {
        return new(UserId.CreateUnique(),
                    firstName,
                    lastName,
                    email,
                    passwordHash,
                    mothlySalary,
                    yearlySalary,
                    createdAt,
                    updatedAt,
                    lastLoginAt,
                    isActive,
                    subscriptionId,
                    cityId);
    }
}
