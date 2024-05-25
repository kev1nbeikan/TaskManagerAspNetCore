using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using User.Infastructure;

namespace WebApplication3.Extentions;

public static class ApiExtentions
{
    public static void AddApiAuthentication(this IServiceCollection services,
        JwtOptions? jwtOptions)
    {
        Console.WriteLine("123213" + jwtOptions?.SecretKey!);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
            JwtBearerDefaults.AuthenticationScheme,
            options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions?.SecretKey!))
                };

                options.Events = new JwtBearerEvents
                {
                    
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("OnTokenValidated");
                        Console.WriteLine(context.Principal?.Identity?.IsAuthenticated);
                        return Task.CompletedTask;
                    },
                    
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("OnAuthenticationFailed");
                        Console.WriteLine(context.Principal?.Identity?.IsAuthenticated);
                        return Task.CompletedTask;
                    },
                    
                    OnForbidden = context =>
                    {
                        Console.WriteLine("OnForbidden");
                        Console.WriteLine(context.Request.HttpContext.User.Identity?.IsAuthenticated);
                        return Task.CompletedTask;
                    },
                    
                    OnChallenge = context =>
                    {
                        Console.WriteLine("OnChallenge");
                        Console.WriteLine(context.Response.Headers.Authorization);
                        Console.WriteLine(context.Request.HttpContext.User.Identity?.IsAuthenticated);
                        return Task.CompletedTask;
                    },
                    
                    OnMessageReceived = context =>
                    {
                        Console.WriteLine("OnMessageReceived");
                        context.Token = context.Request.Cookies["token--cookies"];
                        Console.WriteLine(context.Request.HttpContext.User.Identity?.IsAuthenticated);
                        return Task.CompletedTask;
                    }
                };
            });
        services.AddAuthorization();
    }
}