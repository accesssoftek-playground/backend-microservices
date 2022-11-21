// ReSharper disable CollectionNeverUpdated.Global
namespace AdminToolRootService.Config;

internal sealed class CorsPolicyOptions
{
    public const string SectionName = "CorsPolicy";

    public List<string> Origins { get; set; } = new();
    public List<string> Headers { get; set; } = new();
    public List<string> Methods { get; set; } = new();
}