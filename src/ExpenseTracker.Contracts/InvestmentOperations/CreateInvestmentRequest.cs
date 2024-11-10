namespace ExpenseTracker.Contracts.InvestmentOperations;

public record CreateInvestmentRequest(
    string Name,
    decimal Amount,
    Guid CategoryId);