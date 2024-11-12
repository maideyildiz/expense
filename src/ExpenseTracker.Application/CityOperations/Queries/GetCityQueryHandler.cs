using ErrorOr;

using ExpenseTracker.Application.CityOperations.Common;
using ExpenseTracker.Application.Common.Interfaces.Services;

using MediatR;

namespace ExpenseTracker.Application.CityOperations.Queries;


public class GetCityQueryHandler : IRequestHandler<GetCityQuery, ErrorOr<GetCityResult>>
{
    private readonly ICityService _cityService;

    public GetCityQueryHandler(ICityService cityService)
    {
        _cityService = cityService;
    }

    public Task<ErrorOr<GetCityResult>> Handle(GetCityQuery query, CancellationToken cancellationToken)
    {
        return _cityService.GetCityByIdAsync(query.Id);
    }
}