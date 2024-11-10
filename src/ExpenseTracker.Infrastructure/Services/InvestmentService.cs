
using ErrorOr;

using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.InvestmentOperations.Commands.Create;

namespace ExpenseTracker.Infrastructure.Services;

public class InvestmentService : IInvestmentService
{
    public Task<ErrorOr<int>> AddInvestmentAsync(CreateInvestmentCommand query, Guid userId)
    {
        throw new NotImplementedException();
    }
}