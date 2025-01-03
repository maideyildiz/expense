using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using ExpenseTracker.Contracts.ExpenseOperations;
using MapsterMapper;
using MediatR;
using ExpenseTracker.Application.ExpenseOperations.Commands;
using ExpenseTracker.Application.ExpenseOperations.Queries;
using ExpenseTracker.Application.ExpenseOperations.Commands.Create;
using ExpenseTracker.Application.ExpenseOperations.Commands.Update;
using ExpenseTracker.Application.ExpenseOperations.Commands.Delete;

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
    // [HttpGet]
    // public async Task<IActionResult> ExpenseFeed([FromQuery] ExpenseFeedRequest request)
    // {
    //     var query = _mapper.Map<ExpenseFeedQuery>(request);
    //     var result = await _mediator.Send(query);

    //     return result.Match(
    //         successResult => Ok(_mapper.Map<ExpenseFeedResponse>(result.Value)),
    //         error => Problem(statusCode: (int)error.First().Type, detail: error.First().Description));
    // }
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] GetExpensesRequest request)
    {
        var query = _mapper.Map<GetExpensesQuery>(request);
        var result = await _mediator.Send(query);

        return result.Match(
            successResult => Ok(_mapper.Map<GetExpensesResponse>(result.Value)),
            error => Problem(statusCode: (int)error.First().Type, detail: error.First().Description));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var query = new GetExpenseQuery(id);
        var result = await _mediator.Send(query);

        return result.Match(
            successResult => Ok(_mapper.Map<GetExpenseResponse>(result.Value)),
            error => Problem(statusCode: (int)error.First().Type, detail: error.First().Description));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateExpenseRequest request)
    {
        var command = _mapper.Map<CreateExpenseCommand>(request);
        var result = await _mediator.Send(command);

        return result.Match(
            successResult => Ok(_mapper.Map<GetExpenseResponse>(result.Value)),
            errors => Problem(errors));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateExpenseRequest request)
    {
        var command = new UpdateExpenseCommand(id, request.Amount, request.Description, request.CategoryId);
        var result = await _mediator.Send(command);

        return result.Match(
            successResult => Ok(_mapper.Map<GetExpenseResponse>(result.Value)),
            errors => Problem(statusCode: (int)errors.First().Type, detail: errors.First().Description));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteExpenseCommand(id);
        var result = await _mediator.Send(command);

        return result.Match(
            successResult => Ok(new DeleteExpenseResponse(result.Value)),
            errors => Problem(statusCode: (int)errors.First().Type, detail: errors.First().Description));
    }
}