using ErrorOr;

using ExpenseTracker.Application.CityOperations.Common;

using MediatR;

namespace ExpenseTracker.Application.CityOperations.Queries;


public class GetCityQueryHandler : IRequestHandler<GetCityQuery, ErrorOr<GetCityResult>>
{
    public Task<ErrorOr<GetCityResult>> Handle(GetCityQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}