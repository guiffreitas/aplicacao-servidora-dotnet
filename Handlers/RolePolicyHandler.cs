using aplicacao_servidora_dotnet.Security;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace aplicacao_servidora_dotnet.Handlers
{
    public class RolePolicyHandler : AuthorizationHandler<RolePolicyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RolePolicyRequirement requirement)
        {
            var roleClaim = context?.User.FindFirst(claim => claim.Type == ClaimTypes.Role);

            if(roleClaim == null || requirement == null) 
            {
                context?.Fail();
                return Task.CompletedTask;
            }

            if (requirement.Roles!.Contains(roleClaim?.Value))
            {
                context?.Succeed(requirement);
                return Task.CompletedTask;
            }

            context?.Fail();
            return Task.CompletedTask;
        }
    }
}
