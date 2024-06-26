﻿namespace Linn.Authorisation.Service.Modules
{
    using System.Threading.Tasks;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Facade.Services;

    using Linn.Authorisation.Resources;
    using Linn.Authorisation.Service.Extensions;
    using Linn.Common.Facade;
    using Linn.Common.Service.Core;
    using Linn.Common.Service.Core.Extensions;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class GroupModule : IModule
    {
        public void MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/authorisation/groups", this.GetAll);
            endpoints.MapGet("/authorisation/groups/{id:int}", this.GetGroup);
            endpoints.MapPost("/authorisation/groups", this.CreateGroup);
            endpoints.MapPut("/authorisation/groups/{id:int}", this.UpdateGroup);
            endpoints.MapPost("/authorisation/members", this.AddMember);
        }

        private async Task GetAll(
            HttpResponse res,
            IFacadeResourceService<Group, int, GroupResource, GroupResource> groupService)
        {
            await res.Negotiate(groupService.GetAll());
        }

        private async Task GetGroup(
            HttpResponse res,
            IFacadeResourceService<Group, int, GroupResource, GroupResource> groupService,
            int id)
        {
            await res.Negotiate(groupService.GetById(id));
        }

        private async Task CreateGroup(
            HttpResponse res,
            HttpRequest req,
            GroupResource resource,
            IFacadeResourceService<Group, int, GroupResource, GroupResource> groupService)
        {
            var result = groupService.Add(resource);

            await res.Negotiate(result);
        }

        private async Task UpdateGroup(
            HttpResponse res,
            int id,
            GroupResource resource,
            IFacadeResourceService<Group, int, GroupResource, GroupResource> groupService)
        {

            await res.Negotiate(groupService.Update(id, resource));
        }

        private async Task AddMember(
            HttpResponse res,
            HttpRequest req,
            MemberResource resource,
            IMembersFacadeService service)
        {
            await res.Negotiate(service.AddIndividualMember(resource, req.HttpContext.User.GetEmployeeUrl()));
        }
    }
}
