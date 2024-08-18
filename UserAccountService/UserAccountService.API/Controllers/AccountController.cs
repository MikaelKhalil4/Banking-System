using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using UserAccountService.Application.Features.Accounts.Command;
using UserAccountService.Application.Features.Odata.Queries;
using UserAccountService.Domain.Entities;

namespace UserAccountService.API.Controllers;

public class AccountController : ApiControllerBase
{
    
    [HttpPost("Create()")]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommand command)
    {
        var response = await Mediator.Send(command);
        
        if (response.Success)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
    
    //upadte: api/Account/{id}/setting/update()
    //frombody is to be use to update or create => post
    //fromquery for optional parameter => Get and fromroute for get required
    
         
    [HttpGet,EnableQuery]
    public async Task<ActionResult> Get()
    {
        return Ok(await Mediator.Send(new GetOdataQuery()
        {
            Type = typeof(Account)
        }));
    }
}