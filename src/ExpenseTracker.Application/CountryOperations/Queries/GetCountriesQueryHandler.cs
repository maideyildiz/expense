using ExpenseTracker.Application.Common.Interfaces.Services;
using ExpenseTracker.Application.Common.Models;
using ExpenseTracker.Application.CountryOperations.Common;
using ErrorOr;
using MediatR;

namespace ExpenseTracker.Application.CountryOperations.Queries;

public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, ErrorOr<PagedResult<GetCountryResult>>>
{
    private readonly ICountryService _countryService;

    public GetCountriesQueryHandler(ICountryService countryService)
    {
        _countryService = countryService;
    }

    public async Task<ErrorOr<PagedResult<GetCountryResult>>> Handle(GetCountriesQuery query, CancellationToken cancellationToken)
    {
        var (items, totalCount) = await _countryService.GetCountriesAsync(query.Page, query.PageSize);

        return new PagedResult<GetCountryResult>(items.ToList(), totalCount, query.Page, query.PageSize);
    }
}
