namespace Linn.Authorisation.Service.Modules
{
    using System.Threading.Tasks;

    using Linn.Authorisation.Resources;
    using Linn.Authorisation.Facade.Services;
    using Linn.Authorisation.Service.Extensions;
    using Linn.Common.Service.Core;
    using Linn.Common.Service.Core.Extensions;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Linn.Authorisation.Domain;
    using Linn.Common.Facade;

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
            IFacadeResourceService<Privilege, int, PrivilegeResource, PrivilegeResource> service,
            int id)
        {
            await res.Negotiate(service.GetById(id, req.HttpContext.GetPrivileges()));
        }

        private async Task GetAll(
            HttpRequest req,
            HttpResponse res,
            IFacadeResourceService<Privilege, int, PrivilegeResource, PrivilegeResource> service)
        {
            await res.Negotiate(service.GetAll(req.HttpContext.GetPrivileges()));
        }

        private async Task UpdatePrivilege(
            HttpRequest req,
            HttpResponse res,
            int id,
            PrivilegeResource resource,
            IFacadeResourceService<Privilege, int, PrivilegeResource, PrivilegeResource> service)
        {
            await res.Negotiate(service.Update(id, resource, req.HttpContext.GetPrivileges()));
        }

        private async Task CreatePrivilege(
            HttpResponse res,
            HttpRequest req,
            PrivilegeResource resource,
            IFacadeResourceService<Privilege, int, PrivilegeResource, PrivilegeResource> service)
        {
            var result = service.Add(resource, req.HttpContext.GetPrivileges());

            await res.Negotiate(result);
        }
    }
}
