namespace AdminToolRootService.Config;

internal sealed class ConfigStorageOptions
{
    public const string SectionName = "ConfigStorage";
    
    public string InvokerRoleArn { get; set; } = string.Empty;
    public string ClientApi { get; set; } = string.Empty;
    public string AdminApi { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string ComponentId { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
}