using AdminToolRootService.Authentication;

namespace AdminToolRootService;

internal sealed class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _environment;
    
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var keycloakConfigSection = _configuration.GetSection(KeycloakServiceOptions.SectionName);
        services.Configure<KeycloakServiceOptions>(keycloakConfigSection);
        
        var keycloakConfig = keycloakConfigSection.Get<KeycloakServiceOptions>();
        
        
        services.AddAuthenticationWithJwt(_environment.IsDevelopment(), keycloakConfig);
        services.AddControllers();
    }
    
    public void Configure(IApplicationBuilder app)
    {
        if (_environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });        
    }
}