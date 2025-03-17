using Microsoft.AspNetCore.Mvc;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Application.UseCases.Login.DoLogin;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        [FromServices] IDoLoginUseCase useCase,
        [FromBody] RequestLoginJson request
    )
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}