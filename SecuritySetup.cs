using aplicacao_servidora_dotnet.Handlers;
using aplicacao_servidora_dotnet.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace aplicacao_servidora_dotnet
{
    public static class SecuritySetup
    {
        public static void SetupAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                // Id do Diretório dos aplicativos no Entra ID
                var tenantId = configuration.GetValue<string>("EntraID:TenantId");

                // Id do aplicativo da API servidora no Entra ID
                var serverClientId = configuration.GetValue<string>("EntraID:Servidor:Id");

                // Id do aplicativo da API consumidora no Entra ID
                var consumerClientId = configuration.GetValue<string>("EntraID:Consumidor:Id");

                options.Authority = $"https://login.microsoftonline.com/{tenantId}/v2.0";

                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = $"https://sts.windows.net/{tenantId}/",
                    ValidateAudience = true,
                    ValidAudiences = new List<string> { serverClientId, consumerClientId },
                    ValidateLifetime = true,
                };
            });
        }

        public static void SetupAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ValidConsumer", policy =>
                {
                    policy.AddRequirements(new RoleRequirement());
                });
            });

            services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler>();
        }

    }
}
