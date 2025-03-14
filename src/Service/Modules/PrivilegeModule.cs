using Linn.Authorisation.Domain.Services;

namespace Linn.Authorisation.Service.Modules
{
    using System.Threading.Tasks;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Resources;
    using Linn.Authorisation.Service.Extensions;
    using Linn.Common.Facade;
    using Linn.Common.Service.Core;
    using Linn.Common.Service.Core.Extensions;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class PrivilegeModule : IModule
    {
        public void MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/authorisation/privileges", this.GetAll);
            endpoints.MapPost("/authorisation/privileges", this.CreatePrivilege);
            endpoints.MapGet("/authorisation/privileges/{id:int}", this.GetPrivilege);
            endpoints.MapPut("/authorisation/privileges/{id:int}", this.UpdatePrivilege);
        }

        private async Task GetPrivilege(
            HttpRequest req,
            HttpResponse res,
            IPrivilegeService service,
            int id)
        {
            await res.Negotiate(service.GetPrivilegeById(id, req.HttpContext.GetPrivileges()));
        }

        private async Task GetAll(
            HttpRequest req,
            HttpResponse res,
            IPrivilegeService service)
        {
            await res.Negotiate(service.GetPrivilegesForPermission(req.HttpContext.GetPrivileges()));
        }

        private async Task UpdatePrivilege(
            HttpResponse res,
            int id,
            PrivilegeResource resource,
            IFacadeResourceService<Privilege, int, PrivilegeResource, PrivilegeResource> service)
        {
            
            await res.Negotiate(service.Update(id, resource)); 
        }


        private async Task CreatePrivilege(
            HttpResponse res,
            HttpRequest req,
            PrivilegeResource resource,
            IFacadeResourceService<Privilege, int, PrivilegeResource, PrivilegeResource> service)
        {
            var result = service.Add(resource);

            var authenticatedUser = req.HttpContext.User.GetEmployeeUrl();

            await res.Negotiate(result);
        }
    }
}
