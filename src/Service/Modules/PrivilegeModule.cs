using Linn.Authorisation.Domain.Services;
using Linn.Authorisation.Facade.Services;

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
            IPrivilegeFacadeService service,
            int id)
        {
            await res.Negotiate(service.GetPrivilegeById(id, req.HttpContext.GetPrivileges()));
        }

        private async Task GetAll(
            HttpRequest req,
            HttpResponse res,
            IPrivilegeFacadeService service)
        {
            await res.Negotiate(service.GetAllPrivilegesForUser(req.HttpContext.GetPrivileges()));
        }

        private async Task UpdatePrivilege(
            HttpRequest req,
            HttpResponse res,
            int id,
            PrivilegeResource resource,
            IPrivilegeFacadeService service)
        {
            await res.Negotiate(service.UpdatePrivilege(id, resource, req.HttpContext.GetPrivileges()));
        }



        private async Task CreatePrivilege(
            HttpResponse res,
            HttpRequest req,
            PrivilegeResource resource,
            IPrivilegeFacadeService service)
        {
            var result = service.CreatePrivilege(resource, req.HttpContext.GetPrivileges());

            await res.Negotiate(result);
        }
    }
}
