namespace ExpenseTracker.Contracts.InvestmentOperations;
public record GetInvestmentResponse(
    Guid Id,
    decimal Amount,
    DateTime CreatedAt,
    string Description,
    string CategoryName
);