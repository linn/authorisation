namespace Linn.Authorisation.Service.Modules
{
    using System.Threading.Tasks;

    using Linn.Authorisation.Facade.Services;
    using Linn.Authorisation.Resources;
    using Linn.Authorisation.Service.Extensions;
    using Linn.Common.Service.Core;
    using Linn.Common.Service.Core.Extensions;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class PermissionsModule : IModule
    {
        public void MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/authorisation/permissions", this.GetAllPermissionsForUser);
            endpoints.MapGet("/authorisation/permissions/{groupId:int}", this.GetAllPermissionsForGroup);
            endpoints.MapGet("/authorisation/permissions/privilege", this.GetPermissionsForPrivilege);
            endpoints.MapPost("/authorisation/permissions", this.CreatePermission);
            endpoints.MapDelete("/authorisation/permissions/{id:int}", this.DeletePermission);
        }

        private async Task GetAllPermissionsForUser(
            HttpResponse res,
            HttpRequest req,
            string who,
            IPermissionFacadeService service)
        {
            if (!string.IsNullOrEmpty(who))
            {
                await res.Negotiate(service.GetAllPermissionsForUser(who, req.HttpContext.GetPrivileges()));
            }
        }

        private async Task GetAllPermissionsForGroup(
            HttpResponse res,
            int groupId,
            IPermissionFacadeService service)
        {
            await res.Negotiate(service.GetAllPermissionsForGroup(groupId));
        }

        private async Task GetPermissionsForPrivilege(
            HttpResponse res,
            HttpRequest req,
            int privilegeId,
            IPermissionFacadeService service)
        {
            await res.Negotiate(service.GetPermissionsForPrivilege(privilegeId, req.HttpContext.GetPrivileges()));
        }

        private async Task CreatePermission(
            HttpResponse res,
            HttpRequest req,
            PermissionResource resource,
            IPermissionFacadeService service)
        {
            if (resource.GranteeGroupId == null)
            {
                await res.Negotiate(service.CreateIndividualPermission(resource, req.HttpContext.User.GetEmployeeUrl(), req.HttpContext.GetPrivileges()));
            }
            else
            {
                await res.Negotiate(service.CreateGroupPermission(resource, req.HttpContext.User.GetEmployeeUrl(), req.HttpContext.GetPrivileges()));
            }
        }

        private async Task DeletePermission(
            HttpResponse res,
            HttpRequest req,
            int id,
            IPermissionFacadeService service)
        {
            await res.Negotiate(service.DeletePermission(id, req.HttpContext.GetPrivileges()));
        }
    }
}
