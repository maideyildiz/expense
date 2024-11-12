
using ErrorOr;

using ExpenseTracker.Application.CountryOperations.Common;

using MediatR;

namespace ExpenseTracker.Application.CountryOperations.Queries;

public class GetCountryQueryHandler : IRequestHandler<GetCountryQuery, ErrorOr<GetCountryResult>>
{
    public Task<ErrorOr<GetCountryResult>> Handle(GetCountryQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
