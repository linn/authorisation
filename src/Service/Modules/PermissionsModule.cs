namespace Linn.Authorisation.Service.Modules
{
    using System.Threading.Tasks;

    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Facade.Services;
    using Linn.Authorisation.Resources;
    using Linn.Authorisation.Service.Extensions;
    using Linn.Common.Facade;
    using Linn.Common.Service.Core;
    using Linn.Common.Service.Core.Extensions;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class PermissionsModule : IModule
    {
        public void MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/authorisation/permissions", this.GetPermissions);
            endpoints.MapPost("/authorisation/permissions", this.CreatePermission);
        }

        private async Task GetPermissions(
            HttpResponse res,
            string who,
            int? privilegeId,
            IPermissionFacadeService service)
        {
            if (!string.IsNullOrEmpty(who))
            {
                await res.Negotiate(service.GetAllPermissionsForUser(who));
            }
            else if (privilegeId.HasValue)
            {
                await res.Negotiate(service.GetPermissionsForPrivilege(privilegeId.Value));
            }
            
        }

        private async Task CreatePermission(
            HttpResponse res,
            HttpRequest req,
            PermissionResource resource,
            IPermissionFacadeService service)
        {
            if (resource.GranteeGroupId == null) 
            {
                await res.Negotiate(service.CreateIndividualPermission(resource, req.HttpContext.User.GetEmployeeUrl()));
            }
            else
            {
                await res.Negotiate(service.CreateGroupPermission(resource, req.HttpContext.User.GetEmployeeUrl()));
            }
        }
    }
}
