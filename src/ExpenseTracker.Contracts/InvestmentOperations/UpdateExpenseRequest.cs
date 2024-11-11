namespace ExpenseTracker.Contracts.InvestmentOperations;

public record UpdateInvestmentRequest(
    decimal Amount,
    string Description,
    Guid CategoryId
);