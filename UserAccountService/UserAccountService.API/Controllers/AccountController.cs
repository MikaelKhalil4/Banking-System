using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using UserAccountService.Application.Features.Accounts.Command;
using UserAccountService.Application.Features.Odata.Queries;
using UserAccountService.Application.Helper;
using UserAccountService.Domain.Entities;

namespace UserAccountService.API.Controllers;

public class AccountController : ApiControllerBase
{
    
    [HttpPost("Create()")]
    // [Authorize(Roles = "Employee,Admin")]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommand command)
    {
        var response = await Mediator.Send(command);
        
        if (response.Success)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
    
    
    [HttpPost("Update()")]
    [Authorize(Roles = "Employee,Admin")]
    public async Task<IActionResult> Update([FromBody] UpdateAccountCommand command)
    {

        var result = await Mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
    
    [HttpDelete("{id}")]
// [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAccount(int id)
    {
        var result = await Mediator.Send(new DeleteAccountCommand { AccountId = id });
        if (!result)
            return NotFound();

        return NoContent();
    }

    
         
    [HttpGet,EnableQuery]
    public async Task<ActionResult> Get()
    {
        return Ok(await Mediator.Send(new GetOdataQuery()
        {
            Type = typeof(Account)
        }));
    }
}