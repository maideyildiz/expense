namespace ExpenseTracker.Contracts.InvestmentOperations;
public record GetInvestmentResponse(
    Guid Id,
    decimal Amount,
    string Description,
    DateTime UpdatedAt,
    string CategoryName,
    Guid UserId
);