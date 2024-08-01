using Microsoft.AspNetCore.Authorization;

namespace aplicacao_servidora_dotnet.Security
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public readonly string[] Roles = ["api_access"];
    }
}
