using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers;

[Route("category")]
public class CategoryController : ApiController
{
    [HttpGet("investment")]
    public async Task<IActionResult> GetInvestmentCategories()
    {
        return Ok();
    }
    [HttpGet("investment/{id}")]
    public async Task<IActionResult> GetInvestmentCategory([FromRoute] Guid id)
    {
        return Ok();
    }
    [HttpGet("expense")]
    public async Task<IActionResult> GetExpenseCategories()
    {
        return Ok();
    }

    [HttpGet("expense/{id}")]
    public async Task<IActionResult> GetExpenseCategory([FromRoute] Guid id)
    {
        return Ok();
    }
}