using ErrorOr;

using ExpenseTracker.Contracts.ExpenseOperations;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Post(CreateExpenseRequest request)
    {
        var command = _mapper.Map<CreateExpenseRequest>(request);
        ErrorOr<int> result = (ErrorOr<int>)await _mediator.Send(command);

        return result.Match(
            authResult => Ok(_mapper.Map<int>(result)),
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