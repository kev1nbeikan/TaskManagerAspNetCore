using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using User.Infastructure;

namespace WebApplication3.Extentions;

public static class ApiExtentions
{
    public static void AddApiAuthentication(this IServiceCollection services,
        JwtOptions? jwtOptions)
    {
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
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["token--cookies"];
                        Console.WriteLine(context.Request.HttpContext.User.Identity?.IsAuthenticated);
                        return Task.CompletedTask;
                    },
                };
            });


        services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy =>
                    policy.RequireClaim("Admin", "true")
                );

                options.AddPolicy("StudentPolicy", policy =>
                    policy.RequireClaim("Student", "true")
                );
            }
        );
    }
}