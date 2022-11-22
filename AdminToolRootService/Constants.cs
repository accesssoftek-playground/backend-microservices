using Microsoft.AspNetCore.Mvc;

namespace AdminToolRootService;

internal static class Constants
{
    public static class OpenApi
    {
        public const string Version = "v1";
        public const string Title = "AdminToolRootService";
        public static readonly ApiVersion DefaultApiVersion = new(1, 0);
    }
    
    public static class CorsPolicyName
    {
        public const string AllowAll = nameof(AllowAll);
        public const string Production = nameof(Production);
    } 
}