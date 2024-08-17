using EventLogService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using TransactionService.Application.Features.Odata.Queries;

namespace EventLogService.API.Controllers;

public class EventlogController : ApiControllerBase
{
    [HttpGet,EnableQuery]
    public async Task<ActionResult> Get()
    {
        return Ok(await Mediator.Send(new GetOdataQuery()
        {
            Type = typeof(Eventlog)
        }));
    }
}