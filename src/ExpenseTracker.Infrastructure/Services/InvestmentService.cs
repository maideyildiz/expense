
using ErrorOr;

using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.InvestmentOperations.Commands.Create;
using ExpenseTracker.Application.InvestmentOperations.Common;

namespace ExpenseTracker.Infrastructure.Services;

public class InvestmentService : IInvestmentService
{
    public Task<ErrorOr<InvestmentResult>> AddInvestmentAsync(CreateInvestmentCommand query, Guid userId)
    {
        throw new NotImplementedException();
    }
}