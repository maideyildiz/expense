using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using MapsterMapper;
using ExpenseTracker.Contracts.CountryOperations;
using ExpenseTracker.Application.CountryOperations.Queries;

namespace ExpenseTracker.API.Controllers;


[Route("country")]
[AllowAnonymous]
public class CountryController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public CountryController(ISender mediator, IMapper mapper)
    {
        this._mediator = mediator;
        this._mapper = mapper;
    }
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] GetCountriesRequest request)
    {
        var query = _mapper.Map<GetCountriesQuery>(request);
        var result = await _mediator.Send(query);

        return result.Match(
            successResult => Ok(_mapper.Map<GetCountriesResponse>(result.Value)),
            error => Problem(statusCode: (int)error.First().Type, detail: error.First().Description));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var query = new GetCountryQuery(id);
        var result = await _mediator.Send(query);

        return result.Match(
            successResult => Ok(_mapper.Map<GetCountryResponse>(result.Value)),
            error => Problem(statusCode: (int)error.First().Type, detail: error.First().Description));
    }
}