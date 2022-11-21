// ReSharper disable UnusedMember.Global

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AdminToolRootService.Controllers;

// https://cf.mfmnow.com/display/Automation/Herald+API+-+Service+Status+Endpoints+standard

[ApiController]
[Route("/api/herald/[action]")]
[Produces("application/json")]
[AllowAnonymous]
[EnableCors(Constants.CorsPolicyName.AllowAll)]
public class HeraldController
{
    [HttpGet(Name = "ping")]
    public Task<ActionResult> Ping()
    {
        return Task.FromResult<ActionResult>(new OkObjectResult(new { Result = "OK" }));
    }
    
    [HttpGet(Name = "version")]
    public Task<ActionResult> Version()
    {
        return Task.FromResult<ActionResult>(new OkObjectResult(new { Version = "0.0.1.0" }));
    }
}