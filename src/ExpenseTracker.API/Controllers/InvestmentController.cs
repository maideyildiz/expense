using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using ExpenseTracker.Contracts.InvestmentOperations;
using MapsterMapper;
using MediatR;
using ExpenseTracker.Application.InvestmentOperations.Commands.Create;
using ExpenseTracker.Application.InvestmentOperations.Commands;
using ExpenseTracker.Application.InvestmentOperations.Commands.Update;
using ExpenseTracker.Application.InvestmentOperations.Commands.Delete;
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

    // [HttpGet]
    // public async Task<IActionResult> InvestmentFeed([FromQuery] GetInvestmentsRequest request)
    // {
    //     var query = _mapper.Map<GetInvestmentsQuery>(request);
    //     var result = await _mediator.Send(query);

    //     return result.Match(
    //         successResult => Ok(_mapper.Map<GetInvestmentsResponse>(result.Value)),
    //         error => Problem(statusCode: (int)error.First().Type, detail: error.First().Description));
    // }
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] GetInvestmentsRequest request)
    {
        var query = _mapper.Map<GetInvestmentsQuery>(request);
        var result = await _mediator.Send(query);

        return result.Match(
            successResult => Ok(_mapper.Map<GetInvestmentsResponse>(result.Value)),
            error => Problem(statusCode: (int)error.First().Type, detail: error.First().Description));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var query = new GetInvestmentQuery(id);
        var result = await _mediator.Send(query);

        return result.Match(
            successResult => Ok(_mapper.Map<GetInvestmentResponse>(result.Value)),
            error => Problem(statusCode: (int)error.First().Type, detail: error.First().Description));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateInvestmentRequest request)
    {
        var command = _mapper.Map<CreateInvestmentCommand>(request);
        var result = await _mediator.Send(command);

        return result.Match(
            successResult => Ok(_mapper.Map<GetInvestmentResponse>(result.Value)),
            errors => Problem(errors));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateInvestmentRequest request)
    {
        var command = new UpdateInvestmentCommand(id, request.Amount, request.Description, request.CategoryId);
        var result = await _mediator.Send(command);

        return result.Match(
            successResult => Ok(_mapper.Map<GetInvestmentResponse>(result.Value)),
            errors => Problem(statusCode: (int)errors.First().Type, detail: errors.First().Description));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteInvestmentCommand(id);
        var result = await _mediator.Send(command);

        return result.Match(
            successResult => Ok(new DeleteInvestmentResponse(result.Value)),
            errors => Problem(statusCode: (int)errors.First().Type, detail: errors.First().Description));
    }
}