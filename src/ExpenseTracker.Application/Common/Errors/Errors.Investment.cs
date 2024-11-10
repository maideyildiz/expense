namespace ExpenseTracker.Application.Common.Errors;
using ErrorOr;
public static partial class Errors
{
    public static class Investment
    {
        public static Error InvestmentNotFound => Error.NotFound("Investment.InvestmentNotFound", "Investment not found.");
        public static Error AmountMustBeGreaterThanZero => Error.Conflict("Investment.AmountMustBeGreaterThanZero", "The expense amount must be greater than zero.");
        public static Error InvestmentMustHaveACategory => Error.NotFound("Investment.InvestmentMustHaveACategory", "Expense must have a category.");
        public static Error InvestmentCreationFailed => Error.Conflict("Investment.InvestmentCreationFailed", "Investment creation failed.");
        public static Error InvestmentUpdateFailed => Error.Conflict("Investment.InvestmentUpdateFailed", "Investment update failed.");

    }
}