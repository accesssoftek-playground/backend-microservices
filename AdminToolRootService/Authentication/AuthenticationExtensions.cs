using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AdminToolRootService.Authentication;

internal static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthenticationWithJwt(this IServiceCollection services, bool isDevelopment, KeycloakServiceOptions keycloakOptions)
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
                        ValidIssuer = keycloakOptions.Issuer,
                        ValidAudience = keycloakOptions.Audience,
                        IssuerSigningKey = BuildRsaKey(keycloakOptions.PublicKey)
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