using ErrorOr;

using MediatR;

namespace ExpenseTracker.Application.InvestmentOperations.Commands;


public class DeleteInvestmentCommandHandler : IRequestHandler<DeleteInvestmentCommand, ErrorOr<int>>
{

    Task<ErrorOr<int>> IRequestHandler<DeleteInvestmentCommand, ErrorOr<int>>.Handle(DeleteInvestmentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}