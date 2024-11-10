using ErrorOr;

using ExpenseTracker.Application.InvestmentOperations.Commands;

namespace ExpenseTracker.Application.Common.Interfaces.Services;


public interface IInvestmentService
{
    Task<ErrorOr<int>> AddInvestmentAsync(CreateInvestmentCommand query, Guid userId);
}