using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AdminToolRootService.Authentication;

internal static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthenticationWithJwt(this IServiceCollection services, bool isDevelopment, string publicKeyJwt)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = !isDevelopment;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        // TODO: Move to config
                        ValidIssuer = "http://localhost:6080/realms/AdminToolPrototype",
                        ValidAudience = "http://localhost:5000",
                        IssuerSigningKey = BuildRsaKey(publicKeyJwt)
                    };
                    if (isDevelopment)
                    {
                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                Console.WriteLine("Authentication failed: " + context.Exception.Message);
                                return Task.CompletedTask;
                            },
                            OnTokenValidated = context =>
                            {
                                Console.WriteLine("Token validated: " + context.SecurityToken);
                                return Task.CompletedTask;
                            }
                        };
                    }
                });
            
            return services;
        }
    private static RsaSecurityKey BuildRsaKey(string publicKeyJwt)
    {
        var rsa = RSA.Create();
        rsa.ImportSubjectPublicKeyInfo(source: Convert.FromBase64String(publicKeyJwt), bytesRead: out _);
        return new RsaSecurityKey(rsa);
    }
}