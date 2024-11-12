using ErrorOr;

using ExpenseTracker.Application.CityOperations.Common;
using ExpenseTracker.Application.Common.Models;

using MediatR;

namespace ExpenseTracker.Application.CityOperations.Queries;


public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, ErrorOr<PagedResult<GetCityResult>>>
{
    public async Task<ErrorOr<PagedResult<GetCityResult>>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}