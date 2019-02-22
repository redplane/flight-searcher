using System;
using System.Threading.Tasks;
using MainMicroService.Authentications.TokenValidators;
using MainMicroService.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ServiceShared.Models;

namespace MainMicroService.Configs
{
    public class JsonWebTokenConfigs
    {
        /// <summary>
        ///     Register json web token configuration.
        /// </summary>
        public static void Register(IConfiguration configuration, IServiceCollection services)
        {
            // This can be removed after https://github.com/aspnet/IISIntegration/issues/371
            var authenticationBuilder = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

            var jwtOptions = new AppJwtModel();
            configuration
                .GetSection(MainConfigKeyConstant.AppJwt)
                .Bind(jwtOptions);

            authenticationBuilder.AddJwtBearer(o =>
            {
                // You also need to update /wwwroot/app/scripts/app.js
                o.SecurityTokenValidators.Clear();
                o.SecurityTokenValidators.Add(new JwtBearerValidator());

                // Initialize token validation parameters.
                var tokenValidationParameters = new TokenValidationParameters();
                tokenValidationParameters.ValidAudience = jwtOptions.Audience;
                tokenValidationParameters.ValidIssuer = jwtOptions.Issuer;
                tokenValidationParameters.IssuerSigningKey = jwtOptions.SigningKey;

#if DEBUG
                tokenValidationParameters.ValidateLifetime = false;
#endif

                o.TokenValidationParameters = tokenValidationParameters;

                o.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Path.ToString()
                            .StartsWith("/HUB/", StringComparison.InvariantCultureIgnoreCase))
                            context.Token = context.Request.Query["access_token"];
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}