using ExpenseTracker.Core.Common.Base;

namespace ExpenseTracker.Core.Entities;

public class User : Entity
{
    private readonly List<Guid> expenseIds = new();
    private readonly List<Guid> investmentIds = new();
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public decimal MonthlySalary { get; private set; }
    public decimal YearlySalary { get; private set; }
    public DateTime LastLoginAt { get; private set; }
    public bool IsActive { get; private set; }
    public Guid CityId { get; private set; }
    public IReadOnlyList<Guid> ExpenseIds => expenseIds.AsReadOnly();
    public IReadOnlyList<Guid> InvestmentIds => investmentIds.AsReadOnly();

    private User(
        Guid id,
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        decimal monthlySalary,
        decimal yearlySalary,
        DateTime lastLoginAt,
        bool isActive,
        Guid cityId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        MonthlySalary = monthlySalary;
        YearlySalary = yearlySalary;
        LastLoginAt = lastLoginAt;
        IsActive = isActive;
        CityId = cityId;
    }

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        Guid cityId,
        decimal monthlySalary = decimal.Zero,
        decimal yearlySalary = decimal.Zero)
    {
        return new(
            Guid.NewGuid(),
            firstName,
            lastName,
            email,
            passwordHash,
            monthlySalary,
            yearlySalary,
            DateTime.UtcNow,
            isActive: true,
            cityId);
    }
}
