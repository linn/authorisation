namespace Linn.Authorisation.Service.Modules
{
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using Linn.Common.Service.Core;

    using Microsoft.AspNetCore.Routing;
    using System.Threading.Tasks;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Common.Service.Core.Extensions;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Builder;

    public class GroupModule : IModule
    {
        public void MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/authorisation/groups", this.GetAll);
        }
        private async Task GetAll(
            HttpResponse res,
            IFacadeResourceService<Group, int, GroupResource, GroupResource> groupService)
        {
            await res.Negotiate(groupService.GetAll());
        }
    }
}