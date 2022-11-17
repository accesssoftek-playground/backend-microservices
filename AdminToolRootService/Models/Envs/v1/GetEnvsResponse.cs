namespace AdminToolRootService.Models.Envs.v1;

public sealed class GetEnvsResponse
{
    public List<Environment> Environments { get; set; } = new();
}