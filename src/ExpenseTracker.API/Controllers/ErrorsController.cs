using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Error()
    {
        Exception? exception = this.HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        return this.Problem();
    }
}