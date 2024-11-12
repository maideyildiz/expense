using ErrorOr;

using ExpenseTracker.Application.Common.Models;

using MediatR;

namespace ExpenseTracker.Application.CityOperations.Queries;


public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, ErrorOr<PagedResult<GetCitiesResult>>>
{
    public async Task<ErrorOr<PagedResult<GetCitiesResult>>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}