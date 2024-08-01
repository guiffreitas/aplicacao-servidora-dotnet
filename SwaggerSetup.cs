using Microsoft.OpenApi.Models;

namespace aplicacao_servidora_dotnet
{
    public static class SwaggerSetup
    {
        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                var idDiretorio = configuration.GetValue<string>("EntraID:TenantId");
                var escopoSwagger = configuration.GetValue<string>("EntraID:Swagger:Escopo");

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.OAuth2,
                    Description = "Azure AD OAUTH2",
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri($"https://login.microsoftonline.com/{idDiretorio}/oauth2/v2.0/authorize"),
                            TokenUrl = new Uri($"https://login.microsoftonline.com/{idDiretorio}/oauth2/v2.0/token"),
                            Scopes = new Dictionary<string, string>()
                            {
                                {escopoSwagger!, "Autorização do swagger"}
                            }
                        }
                    }
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            },
                            Scheme = "oauth2",
                            Name = "oauth2",
                            In = ParameterLocation.Header
                        },
                        new [] { escopoSwagger }
                    }
                });
            });
        } 
    }
}
