using HotelBooking.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HotelBooking.Infrastructure.DependencyInjection
{
    public static class AuthenticationInjection
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var JwtSectionName = "JwtOptions";
            var jwtOptions = configuration.GetSection(JwtSectionName).Get<JwtOptions>();
            services.Configure<JwtOptions>(configuration.GetSection(JwtSectionName));

            if (jwtOptions == null || string.IsNullOrEmpty(jwtOptions.SecretKey))
            {
                throw new ArgumentNullException("JWT settings are not properly configured.");
            }

            var key = Encoding.ASCII.GetBytes(jwtOptions.SecretKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            return services;
        }
    }
}
