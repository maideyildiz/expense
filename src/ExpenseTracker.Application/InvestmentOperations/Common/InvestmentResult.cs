namespace ExpenseTracker.Application.InvestmentOperations.Common;

public record InvestmentResult(
    Guid Id,
    decimal Amount,
    string Description,
    DateTime UpdatedAt,
    string CategoryName,
    Guid UserId);