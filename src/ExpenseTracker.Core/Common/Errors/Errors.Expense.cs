namespace ExpenseTracker.Core.Common.Errors;
using ErrorOr;
public static partial class Errors
{
    public static class Expense
    {
        public static Error AmountMustBeGreaterThanZero => Error.Conflict("Expense.AmountMustBeGreaterThanZero", "The expense amount must be greater than zero.");
        public static Error ExpenseMustHaveACategory => Error.NotFound("Expense.ExpenseMustHaveACategory", "Expense must have a category.");

    }
}