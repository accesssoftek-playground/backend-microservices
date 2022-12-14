using AdminToolRootService.Authentication;
using AdminToolRootService.Config;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;

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
        
        var corsPolicySection = _configuration.GetSection(CorsPolicyOptions.SectionName);
        services.Configure<CorsPolicyOptions>(corsPolicySection);

        services.AddAuthenticationWithJwt(_environment.IsDevelopment(), keycloakConfigSection.Get<KeycloakServiceOptions>());

        services.AddControllers();
        
        if (_environment.IsDevelopment())
        {
            services.AddCors(options =>
            {
                options.DefaultPolicyName = Constants.CorsPolicyName.AllowAll;
                AddCorsAllowAll(options);
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AdminToolRootService", Version = "v1" });
            });
        }
        else
        {
            services.AddCors(options =>
            {
                options.DefaultPolicyName = Constants.CorsPolicyName.Production;
                AddCorsAllowAll(options);
                options.AddPolicy(Constants.CorsPolicyName.Production, builder => builder
                    .WithOrigins(corsPolicySection.Get<CorsPolicyOptions>().Origins.ToArray())
                    .WithHeaders(corsPolicySection.Get<CorsPolicyOptions>().Headers.ToArray())
                    .WithMethods(corsPolicySection.Get<CorsPolicyOptions>().Methods.ToArray()));
            });
        }
        
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });
        
        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });        
    }

    private static void AddCorsAllowAll(CorsOptions options)
    {
        options.AddPolicy(Constants.CorsPolicyName.AllowAll,
            builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    }

    public void Configure(IApplicationBuilder app)
    {
        if (_environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseCors();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });        
    }
}