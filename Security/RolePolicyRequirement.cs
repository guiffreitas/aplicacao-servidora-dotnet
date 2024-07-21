using Microsoft.AspNetCore.Authorization;

namespace aplicacao_servidora_dotnet.Security
{
    public class RolePolicyRequirement : IAuthorizationRequirement
    {
        public string[]? Roles { get; set; }

        public RolePolicyRequirement(string? roles) 
        {
            Roles = roles?.Split(",");
        }

    }
}
