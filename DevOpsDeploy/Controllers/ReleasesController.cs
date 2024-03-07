using DevOpsDeploy.Application.Releases.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevOpsDeploy.Controllers;

[ApiController]
[Route("[controller]")]
public class ReleasesController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ReleasesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public IActionResult GetReleases([FromQuery] GetReleasesQuery query)
    {
        var response = _mediator.Send(query).Result;
        return Ok(response);
    }
}