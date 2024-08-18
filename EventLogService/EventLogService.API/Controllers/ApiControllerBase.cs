using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventLogService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private IMediator _mediatR = null!;
    protected IMediator Mediator => _mediatR ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

}