using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers;

[Route("category")]
[AllowAnonymous]
public class CategoryController : ApiController
{
    [HttpGet("investment")]
    public async Task<IActionResult> GetInvestmentCategories()
    {
        return Ok();
    }

    [HttpGet("expense")]
    public async Task<IActionResult> GetExpenseCategories()
    {
        return Ok();
    }
}