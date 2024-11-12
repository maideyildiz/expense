
using ErrorOr;

using ExpenseTracker.Application.Common.Models;
using ExpenseTracker.Application.CountryOperations.Common;

using MediatR;

namespace ExpenseTracker.Application.CountryOperations.Queries;

public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, ErrorOr<PagedResult<GetCountryResult>>>
{
    public Task<ErrorOr<PagedResult<GetCountryResult>>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
