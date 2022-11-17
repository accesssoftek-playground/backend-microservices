using AdminToolRootService.Models.Envs.v1;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Environment = AdminToolRootService.Models.Envs.v1.Environment;

namespace AdminToolRootService.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[ApiVersion("1.0")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public sealed class EnvsController : ControllerBase
{
    [HttpGet(Name = "GetEnvironments")]
    public Task<ActionResult<GetEnvsResponse>> Get()
    {
        return Task.FromResult<ActionResult<GetEnvsResponse>>(new GetEnvsResponse
        {
            Environments = new List<Environment>
            {
                new()
                {
                    Id = "dev",
                    Tenants = new List<Tenant> {new() {Id = "tenant1"}}
                },
                new()
                {
                    Id = "prod",
                    Tenants = new List<Tenant> {new() {Id = "tenant1"}, new() {Id = "tenant2"}}
                },
            }
        });
    }
}