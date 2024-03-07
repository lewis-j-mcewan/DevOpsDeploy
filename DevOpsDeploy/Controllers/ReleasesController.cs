using DevOpsDeploy.Application.Releases.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DevOpsDeploy.Controllers;

[ApiController]
[Route("[controller]")]
public class ReleasesController : Controller
{
    
    public ReleasesController()
    {
        
    }
    
    [HttpGet]
    public IActionResult GetReleases([FromQuery] int keep)
    {
        var response = new GetReleasesQuery().Handle(keep);
        return Ok(response);
    }
}