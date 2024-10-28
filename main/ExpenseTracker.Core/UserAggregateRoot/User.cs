using ExpenseTracker.Core.Common.Models;
using ExpenseTracker.Core.ExpenseAggregateRoot.ValueObjests;
using ExpenseTracker.Core.InvestmentAggregateRoot.ValueObjects;
using ExpenseTracker.Core.UserAggregateRoot.Entities;
using ExpenseTracker.Core.UserAggregateRoot.ValueObjects;

namespace ExpenseTracker.Core.UserAggregateRoot;

public class User : AggregateRoot<UserId>
{
    private readonly List<ExpenseId> _expenseIds = new();
    private readonly List<InvestmentId> _investmentIds = new();
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
    public Subscription Subscription { get; }
    public IReadOnlyList<ExpenseId> ExpenseIds => _expenseIds.AsReadOnly();
    public IReadOnlyList<InvestmentId> InvestmentIds => _investmentIds.AsReadOnly();

    private User(UserId id,
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
                Subscription subscription) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        MothlySalary = mothlySalary;
        YearlySalary = yearlySalary;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        LastLoginAt = lastLoginAt;
        IsActive = isActive;
        Subscription = subscription;
    }

    public static User Create(string firstName,
                             string lastName,
                             string email,
                             string passwordHash,
                             decimal mothlySalary,
                             decimal yearlySalary,
                             DateTime createdAt,
                             DateTime updatedAt,
                             DateTime lastLoginAt,
                             bool isActive,
                             Subscription subscription)
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
                    subscription);
    }
}
