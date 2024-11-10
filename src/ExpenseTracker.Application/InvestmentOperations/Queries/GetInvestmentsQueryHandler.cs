using ErrorOr;

using ExpenseTracker.Application.Common.Models;
using ExpenseTracker.Application.InvestmentOperations.Commands;
using ExpenseTracker.Application.InvestmentOperations.Common;

using MediatR;

namespace ExpenseTracker.çApplication.InvestmentOperations.Queries;


public class GetInvestmentsQueryHandler : IRequestHandler<GetInvestmentsQuery, ErrorOr<PagedResult<InvestmentResult>>>
{

    Task<ErrorOr<PagedResult<InvestmentResult>>> IRequestHandler<GetInvestmentsQuery, ErrorOr<PagedResult<InvestmentResult>>>.Handle(GetInvestmentsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}