using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using AdminToolRootService.Authentication;
using AdminToolRootService.Config;
using AdminToolRootService.Services;

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
        
        services.Configure<ConfigStorageOptions>(_configuration.GetSection(ConfigStorageOptions.SectionName));

        services.AddAuthenticationWithJwt(_environment.IsDevelopment(), keycloakConfigSection.Get<KeycloakServiceOptions>());

        services.AddCorsPolicy(_configuration, _environment);
        
        services.AddControllers();
        
        if (_environment.IsDevelopment())
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Constants.OpenApi.Version, new OpenApiInfo { Title = Constants.OpenApi.Title, Version = Constants.OpenApi.Version });
            });
        }
        
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = Constants.OpenApi.DefaultApiVersion;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });
        
        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });

        services.AddScoped<IConfigStorageService, ConfigStorageService>();
    }

    public void Configure(IApplicationBuilder app)
    {
        if (_environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });        
    }
}