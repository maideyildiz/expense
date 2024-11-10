
using ErrorOr;

using MediatR;

namespace ExpenseTracker.Application.InvestmentOperations.Commands;

public class UpdateInvestmentCommandHandler : IRequestHandler<UpdateInvestmentCommand, ErrorOr<int>>
{
    public Task<ErrorOr<int>> Handle(UpdateInvestmentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}