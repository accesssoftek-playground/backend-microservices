using AdminToolRootService.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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

    // TODO: Move to config
    private const string PublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA4uWOVsJGjWQK7aHzjK0OspJwv+/dX4qVXlyPhU0ngi3n1Tpm8fVvJgE6egqb28AYxOB4d3/vE2zeqG/O6JKnoWdliK/IhcX4LrZOxyPPf8eizvahgfqyAyfLrlHAJxnNdjjJsog10pSVjtAtbwH1bF+JtMubdpVSMs1HKEE4Cpbr0ks2gis0Dpq/R33uF9woAgoLfpVk+9cPsde3sJ0k7W9xufmkP4yGZfzuAQs+XEEJtBpgvETBYkKXYidiDuLxQIq0ilYZ0yusY3RbCGKxqhlxzVh0bRswFgGUXZjc/yQRGS1ZUf+Q3WIszfCOFuQqeMUeBYgiou8O1+Z8BJSkPwIDAQAB";
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthenticationWithJwt(_environment.IsDevelopment(), PublicKey);
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