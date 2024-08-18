using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using TransactionService.Application.Features.Odata.Queries;
using TransactionService.Application.Features.Transactions.Command;
using TransactionService.Domain.Entities;

namespace TransactionService.API.Controllers;

public class TransactionController : ApiControllerBase
{
    [HttpPost("AddingNewtransaction")]
    public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionCommand command)
    {

        var response = await Mediator.Send(command);
        
        return Ok(response);
    }


    [HttpGet, EnableQuery]
    public async Task<ActionResult> Get()
    {
        return Ok(await Mediator.Send(new GetOdataQuery()
        {
            Type = typeof(Transaction)
        }));
    }
}