namespace AdminToolRootService.Config;

internal sealed class KeycloakServiceOptions
{
    public const string SectionName = "Keycloak";

    public string Realm { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string PublicKey { get; set; } = string.Empty;
}