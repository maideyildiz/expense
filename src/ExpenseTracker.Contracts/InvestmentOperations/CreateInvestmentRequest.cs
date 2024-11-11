namespace ExpenseTracker.Contracts.InvestmentOperations;

public record CreateInvestmentRequest(
    decimal Amount,
    string Description,
    Guid CategoryId);