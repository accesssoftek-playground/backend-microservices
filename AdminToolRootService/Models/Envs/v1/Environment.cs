namespace AdminToolRootService.Models.Envs.v1;

public sealed class Environment
{
    public string Id { get; set; } = string.Empty;
    public List<Tenant> Tenants { get; set; } = new();
}