using Microsoft.Extensions.DependencyInjection;

namespace Shared.Extensions
{
    public static class CorsExtension
    {
        public static IServiceCollection AddCorsExtension(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    //policy.WithOrigins(new[] { "http://localhost:3000", "http://localhost:8080", "http://localhost:4200" });
                    policy.WithOrigins("*");
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowCredentials();
                });
            });

            return services;
        }
    }
}
