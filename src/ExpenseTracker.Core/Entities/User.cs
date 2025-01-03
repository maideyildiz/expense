using ExpenseTracker.Core.Common.Base;

using System;
using System.Collections.Generic;

namespace ExpenseTracker.Core.Entities
{
    public class User : Entity
    {
        public User() { }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public decimal MonthlySalary { get; private set; }
        public decimal YearlySalary { get; private set; }
        public DateTime LastLoginAt { get; private set; }
        public bool IsActive { get; private set; }
        public Guid CityId { get; private set; }
        public IReadOnlyList<Investment> Investments { get; private set; }
        public IReadOnlyList<Expense> Expenses { get; private set; }
        private List<Investment> investments;
        private List<Expense> expenses;

        private User(
            Guid id,
            string firstName,
            string lastName,
            string email,
            string username,
            string passwordHash,
            decimal monthlySalary,
            decimal yearlySalary,
            DateTime lastLoginAt,
            bool isActive,
            Guid cityId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Username = username;
            PasswordHash = passwordHash;
            MonthlySalary = monthlySalary;
            YearlySalary = yearlySalary;
            LastLoginAt = lastLoginAt;
            IsActive = isActive;
            CityId = cityId;
            investments = new List<Investment>();
            expenses = new List<Expense>();
        }

        public static User Create(
            string firstName,
            string lastName,
            string email,
            string username,
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
                username,
                passwordHash,
                monthlySalary,
                yearlySalary,
                DateTime.UtcNow,
                isActive: true,
                cityId);
        }
        public void Update(
            string? firstName,
            string? lastName,
            string? email,
            string? passwordHash,
            Guid? cityId,
            decimal? monthlySalary,
            decimal? yearlySalary,
            bool? isActive)
        {
            FirstName = firstName is not null ? firstName : FirstName;
            LastName = lastName is not null ? lastName : LastName;
            Email = email is not null ? email : Email;
            PasswordHash = passwordHash is not null ? passwordHash : PasswordHash;
            MonthlySalary = monthlySalary is not null ? monthlySalary.Value : MonthlySalary;
            YearlySalary = yearlySalary is not null ? yearlySalary.Value : YearlySalary;
            LastLoginAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            IsActive = isActive is not null ? isActive.Value : IsActive;
            CityId = cityId is not null ? cityId.Value : CityId;
        }

        public void AddInvestment(Investment investment)
        {
            investments.Add(investment);
        }

        public void AddExpense(Expense expense)
        {
            expenses.Add(expense);
        }

        public IReadOnlyList<Investment> GetInvestments() => investments.AsReadOnly();
        public IReadOnlyList<Expense> GetExpenses() => expenses.AsReadOnly();
    }
}
