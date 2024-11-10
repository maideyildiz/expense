using ErrorOr;

using ExpenseTracker.Application.InvestmentOperations.Commands.Create;
using ExpenseTracker.Application.InvestmentOperations.Commands.Update;
using ExpenseTracker.Application.InvestmentOperations.Common;

namespace ExpenseTracker.Application.Common.Interfaces.Services;


public interface IInvestmentService
{
    Task<ErrorOr<Guid>> AddInvestmentAsync(CreateInvestmentCommand command, Guid userId);
    Task<(IEnumerable<InvestmentResult> Items, int TotalCount)> GetInvestmentsAsync(Guid userId, int page, int pageSize);
    Task<ErrorOr<InvestmentResult>> GetInvestmentByIdAsync(Guid id);
    Task<ErrorOr<InvestmentResult>> UpdateInvestmentAsync(UpdateInvestmentCommand command);
    Task<ErrorOr<bool>> DeleteInvestmentAsync(Guid id);
    Task<bool> CheckIfUserOwnsInvestment(Guid userId, Guid investmentId);
}