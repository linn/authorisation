namespace Linn.Authorisation.Service.Modules
{
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
        }

        private async Task GetAll(
            HttpResponse res,
            IFacadeResourceService<Privilege, int, PrivilegeResource, PrivilegeResource> service)
        {
            await Task.Delay(5000); // just for the loading story - delete before merging!
            await res.Negotiate(service.GetAll());
        }
    }
}
