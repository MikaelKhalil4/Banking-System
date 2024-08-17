using Microsoft.AspNetCore.Mvc;
using UserAccountService.Application.Features.Accounts.Command;

namespace UserAccountService.API.Controllers;

public class AccountController : ApiControllerBase
{
    
    [HttpPost("CreateUserAccount")]
    public async Task<IActionResult> CreateAccount([FromQuery] CreateAccountCommand command)
    {
        var response = await Mediator.Send(command);
        
        if (response.Success)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}