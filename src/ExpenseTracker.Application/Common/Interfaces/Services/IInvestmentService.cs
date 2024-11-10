using ErrorOr;

using ExpenseTracker.Application.InvestmentOperations.Commands.Create;
using ExpenseTracker.Application.InvestmentOperations.Common;

namespace ExpenseTracker.Application.Common.Interfaces.Services;


public interface IInvestmentService
{
    Task<ErrorOr<InvestmentResult>> AddInvestmentAsync(CreateInvestmentCommand query, Guid userId);
}