using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using ExpenseTracker.Contracts.ExpenseOperations;
using MapsterMapper;
using MediatR;
using ExpenseTracker.Application.ExpenseOperations.Commands;

namespace ExpenseTracker.API.Controllers;

[Route("expense")]
public class ExpenseController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public ExpenseController(ISender mediator, IMapper mapper)
    {
        this._mediator = mediator;
        this._mapper = mapper;
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
    public async Task<IActionResult> Post([FromBody] CreateExpenseRequest request)
    {
        var command = _mapper.Map<CreateExpenseCommand>(request);
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