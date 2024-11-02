namespace ExpenseTracker.Contracts.ExpenseOperations;

public record CreateExpenseRequest(
    decimal Amount,
    string Description,
    string CategoryId
);