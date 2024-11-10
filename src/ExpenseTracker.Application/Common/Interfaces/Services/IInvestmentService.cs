using ErrorOr;

using ExpenseTracker.Application.InvestmentOperations.Commands.Create;

namespace ExpenseTracker.Application.Common.Interfaces.Services;


public interface IInvestmentService
{
    Task<ErrorOr<int>> AddInvestmentAsync(CreateInvestmentCommand query, Guid userId);
}