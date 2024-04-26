using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;

namespace PizzeriaAPI.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            string? issuer,
            string? audience,
            string? key
            )
        {
            if (key is null) throw new NullReferenceException("Jwt key is null");

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddCookie("Cookies", options =>
                {
                    options.LoginPath = "/";
                    options.ExpireTimeSpan = TimeSpan.FromDays(7);
                })
                .AddJwtBearer("Bearer", options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                });

            return services;
        }
    }
}
