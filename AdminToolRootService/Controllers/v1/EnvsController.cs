using AdminToolRootService.Models.Envs.v1;
using AdminToolRootService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Environment = AdminToolRootService.Models.Envs.v1.Environment;

namespace AdminToolRootService.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[ApiVersion("1.0")]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[AllowAnonymous]
public sealed class EnvsController : ControllerBase
{
    private readonly IConfigStorageService _configStorageService;
    private readonly ILogger<EnvsController> _logger;

    public EnvsController(IConfigStorageService configStorageService, ILogger<EnvsController> logger)
    {
        _configStorageService = configStorageService;
        _logger = logger;
    }

    [HttpGet(Name = "GetEnvironments")]
    public async Task<ActionResult<GetEnvsResponse>> Get()
    {
        var envs = await _configStorageService.GetEnvironmentsAsync();
        var response = new GetEnvsResponse { Environments = new List<Environment>(envs.Count) };
        var tenantsTasks = new List<Task>(envs.Count);
        
        foreach (var env in envs)
        {
            tenantsTasks.Add(_configStorageService
                .GetTenantsForEnvironmentAsync(env.Code)
                .ContinueWith(task =>
                {
                    response.Environments.Add(new Environment
                    {
                        Id = env.Code,
                        Tenants = task.Result.Select(t => new Tenant { Id = t.Code }).ToList()
                    });
                }));
        }

        await Task.WhenAll(tenantsTasks);
        return Ok(response);
    }
}