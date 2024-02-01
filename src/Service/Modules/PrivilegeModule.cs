namespace Linn.Authorisation.Service.Modules
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Resources;
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
        }

        private async Task GetAll(
            HttpResponse res,
            IFacadeResourceService<Privilege, int, PrivilegeResource, PrivilegeResource> service)
        {
            await res.Negotiate(service.GetAll());
        }

        private async Task UpdatePrivilege(
            HttpResponse res,
            PrivilegeResource resource,
            IFacadeResourceService<Privilege, int, PrivilegeResource, PrivilegeResource> service)
           {}


        private async Task CreatePrivilege(
            HttpResponse res,
            PrivilegeResource resource,
            IFacadeResourceService<Privilege, int, PrivilegeResource, PrivilegeResource> service)
        {
            var result = service.Add(resource);

            await res.Negotiate(result);
        }
    }
}
