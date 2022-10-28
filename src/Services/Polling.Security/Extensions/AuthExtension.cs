using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Polling.Security.Domain;
using System.Text;

namespace Polling.Security.Extensions
{
    public static class AuthExtension
    {
        public static IServiceCollection AddAuthExtension(this IServiceCollection services, IConfiguration configuration)
        {

            JwtOptions jwtOptions = new();
            configuration.Bind("JwtConfigurations", jwtOptions);
            services.AddSingleton(jwtOptions);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.IssuerSigningKey)),
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = jwtOptions.ValidateAudience,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}
