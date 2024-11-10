namespace ExpenseTracker.Application.Common.Errors;
using ErrorOr;
public static partial class Errors
{
    public static class Expense
    {
        public static Error ExpenseNotFound => Error.NotFound("Expense.ExpenseNotFound", "Expense not found.");
        public static Error ExpenseCreationFailed => Error.Conflict("Expense.ExpenseCreationFailed", "Expense creation failed.");
        public static Error AmountMustBeGreaterThanZero => Error.Conflict("Expense.AmountMustBeGreaterThanZero", "The expense amount must be greater than zero.");
        public static Error ExpenseMustHaveACategory => Error.NotFound("Expense.ExpenseMustHaveACategory", "Expense must have a category.");
        public static Error ExpenseUpdateFailed => Error.Conflict("Expense.ExpenseUpdateFailed", "Expense update failed.");

    }
}