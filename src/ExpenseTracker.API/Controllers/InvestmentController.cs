using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using ExpenseTracker.Application.InvestmentOperations.Commands;
using ExpenseTracker.Contracts.InvestmentOperations;
using MapsterMapper;
using MediatR;
namespace ExpenseTracker.API.Controllers;
[Route("investment")]
public class InvestmentController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public InvestmentController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }


    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateInvestmentRequest request)
    {
        var command = _mapper.Map<CreateInvestmentCommand>(request);
        ErrorOr<int> result = await _mediator.Send(command);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update()
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok();
    }
}