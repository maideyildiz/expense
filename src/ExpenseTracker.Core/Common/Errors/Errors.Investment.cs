namespace ExpenseTracker.Core.Common.Errors;
using ErrorOr;
public static partial class Errors
{
    public static class Investment
    {
        public static Error AmountMustBeGreaterThanZero => Error.Conflict("Investment.AmountMustBeGreaterThanZero", "The expense amount must be greater than zero.");
        public static Error ExpenseMustHaveACategory => Error.NotFound("Investment.ExpenseMustHaveACategory", "Expense must have a category.");

    }
}