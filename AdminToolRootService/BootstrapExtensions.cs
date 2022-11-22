using AdminToolRootService.Config;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace AdminToolRootService;

internal static class BootstrapExtensions
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        var corsPolicySection = configuration.GetSection(CorsPolicyOptions.SectionName);
        services.Configure<CorsPolicyOptions>(corsPolicySection);

        if (environment.IsDevelopment())
        {
            services.AddCors(options =>
            {
                options.DefaultPolicyName = Constants.CorsPolicyName.AllowAll;
                AddCorsAllowAll(options);
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

        return services;
    }
    
    private static void AddCorsAllowAll(CorsOptions options)
    {
        options.AddPolicy(Constants.CorsPolicyName.AllowAll,
            builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    }
}