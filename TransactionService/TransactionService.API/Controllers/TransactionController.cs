using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using TransactionService.Application.Features.Odata.Queries;
using TransactionService.Application.Features.Transactions.Command;
using TransactionService.Domain.Entities;

namespace TransactionService.API.Controllers;

public class TransactionController : ApiControllerBase
{
    [HttpPost("Add()")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> CreateTransaction([FromBody] TransactionViewModelRequest desiredTransaction)
    {

        CreateTransactionCommand command = new CreateTransactionCommand
        {
            AccountId = desiredTransaction.AccountId,
            Amount = desiredTransaction.Amount,
            IsRecurrent = false
        };
        
        var response = await Mediator.Send(command);
        
        return Ok(response);
    }

    
    [HttpPost("AddNewRecurrent()")]
    [Authorize(Roles = "Employee")]
    public async Task<IActionResult> CreateRecurrentTransaction([FromBody] TransactionViewModelRequest desiredTransaction)
    {
        CreateTransactionCommand command = new CreateTransactionCommand
        {
            AccountId = desiredTransaction.AccountId,
            Amount = desiredTransaction.Amount,
            IsRecurrent = true
        };

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