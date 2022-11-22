using AS.ConfigStorage.Sdk.Admin;

namespace AdminToolRootService.Services;

public interface IConfigStorageService
{
    Task<List<EnvironmentDto>> GetEnvironmentsAsync();
    Task<List<TenantDto>> GetTenantsForEnvironmentAsync(string environmentCode);
}