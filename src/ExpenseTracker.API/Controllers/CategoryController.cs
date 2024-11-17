using ExpenseTracker.Application.CategoryOperations.Queries;
using ExpenseTracker.Contracts.CategoryOperations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using MediatR;
using ExpenseTracker.Application.CategoryOperations.Common;

namespace ExpenseTracker.API.Controllers;

[Route("category")]
public class CategoryController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    public CategoryController(
        ISender mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    [HttpGet("investment")]
    public async Task<IActionResult> GetInvestmentCategories([FromQuery] GetCategoriesRequest request)
    {
        var query = _mapper.Map<GetCategoriesQuery>(request);
        var result = await _mediator.Send(query);

        return result.Match(
            successResult => Ok(_mapper.Map<GetCategoriesResponse>(result.Value)),
            error => Problem(statusCode: (int)error.First().Type, detail: error.First().Description));
    }
    [HttpGet("investment/{id}")]
    public async Task<IActionResult> GetInvestmentCategory([FromRoute] Guid id)
    {
        var query = new GetCategoryQuery(id);
        var result = await _mediator.Send(query);

        return result.Match(
            successResult => Ok(_mapper.Map<GetCategoryResponse>(result.Value)),
            error => Problem(statusCode: (int)error.First().Type, detail: error.First().Description));
    }
    [HttpGet("expense")]
    public async Task<IActionResult> GetExpensesCategories([FromQuery] GetCategoriesRequest request)
    {
        var query = _mapper.Map<GetCategoriesQuery>(request);
        var result = await _mediator.Send(query);

        return result.Match(
            successResult => Ok(_mapper.Map<GetCategoriesResponse>(result.Value)),
            error => Problem(statusCode: (int)error.First().Type, detail: error.First().Description));
    }

    [HttpGet("expense/{id}")]
    public async Task<IActionResult> GetExpenseCategory([FromRoute] Guid id)
    {
        var query = new GetCategoryQuery(id);
        var result = await _mediator.Send(query);

        return result.Match(
            successResult => Ok(_mapper.Map<GetCategoryResponse>(result.Value)),
            error => Problem(statusCode: (int)error.First().Type, detail: error.First().Description));
    }
}