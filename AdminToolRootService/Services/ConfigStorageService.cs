using AdminToolRootService.Config;
using AS.ConfigStorage.Sdk;
using AS.ConfigStorage.Sdk.Admin;
using AS.ConfigStorage.Sdk.Aws;
using AS.ConfigStorage.Sdk.Client;
using AS.ConfigStorage.Sdk.Common;
using Microsoft.Extensions.Options;

namespace AdminToolRootService.Services;

internal sealed class ConfigStorageService : IConfigStorageService
{
    private readonly ConfigStorageOptions _options;
    private readonly ILogger<ConfigStorageService> _logger;
    private readonly ICredentialsProvider _credentialsProvider;
    private readonly IConfigStorageClient _clientApi;
    private readonly IConfigStorageAdminClient _adminApi;

    public ConfigStorageService(IOptions<ConfigStorageOptions> options, ILogger<ConfigStorageService> logger)
    {
        _options = options.Value;
        _logger = logger;
        _credentialsProvider = new AwsAssumedCredentialsProvider(_options.InvokerRoleArn);
        
        var clientOptions = new ClientOptions
        {
            Region = _options.Region,
            ComponentId = _options.ComponentId,
            ApiKey = _options.ApiKey,
        };
        
        _clientApi = new ConfigStorageClient(_options.ClientApi, _credentialsProvider, clientOptions);
        _adminApi = new ConfigStorageAdminClient(_options.AdminApi, _credentialsProvider, clientOptions);
    }

    public async Task<List<EnvironmentDto>> GetEnvironmentsAsync()
    {
        return (await _adminApi.GetEnvironmentsAsync()).Items;
    }

    public async Task<List<TenantDto>> GetTenantsForEnvironmentAsync(string environmentCode)
    {
        return (await _adminApi.GetEnvironmentTenantsAsync(new GetEnvironmentTenantsRequest(environmentCode))).Items;
    }
}