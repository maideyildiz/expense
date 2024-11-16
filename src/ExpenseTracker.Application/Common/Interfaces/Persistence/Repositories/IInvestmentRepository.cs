using ExpenseTracker.Application.InvestmentOperations.Common;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Application.Common.Interfaces.Persistence.Repositories;

public interface IInvestmentRepository : IBaseRepository<Investment>
{
    Task<InvestmentResult> GetInvestmentByIdAsync(Guid id);
    Task<IEnumerable<InvestmentResult>> GetInvestmentsByUserIdAsync(Guid userId, int page, int pageSize);
    Task<Guid> GetInvestmentUserIdAsync(Guid investmentId);
}