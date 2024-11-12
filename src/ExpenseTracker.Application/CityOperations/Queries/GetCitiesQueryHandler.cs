using ExpenseTracker.Application.CityOperations.Common;
using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.Common.Models;
using ErrorOr;
using MediatR;

namespace ExpenseTracker.Application.CityOperations.Queries;


public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, ErrorOr<PagedResult<GetCityResult>>>
{
    private readonly ICityService _cityService;

    public GetCitiesQueryHandler(ICityService cityService)
    {
        _cityService = cityService;
    }

    public async Task<ErrorOr<PagedResult<GetCityResult>>> Handle(GetCitiesQuery query, CancellationToken cancellationToken)
    {
        var (items, totalCount) = await _cityService.GetCitiesByCountryIdAsync(query.CountryId, query.Page, query.PageSize);

        return new PagedResult<GetCityResult>(items.ToList(), totalCount, query.Page, query.PageSize);
    }
}