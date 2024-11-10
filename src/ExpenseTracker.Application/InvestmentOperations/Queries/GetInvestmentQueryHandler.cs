
using ErrorOr;

using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.InvestmentOperations.Common;

using MediatR;

namespace ExpenseTracker.Application.InvestmentOperations.Commands;

public class GetInvestmentQueryHandler : IRequestHandler<GetInvestmentQuery, ErrorOr<InvestmentResult>>
{
    private readonly IInvestmentService _investmentService;

    public GetInvestmentQueryHandler(IInvestmentService investmentService)
    {
        _investmentService = investmentService;
    }

    public Task<ErrorOr<InvestmentResult>> Handle(GetInvestmentQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}