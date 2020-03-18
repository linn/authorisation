namespace Linn.Authorisation.Service.Modules
{
    using System.Linq;
    using Common.Facade;
    using Facade;
    using Linn.Common.Authorisation;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Service.Extensions;
    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Security;
    using Resources;

    public sealed class GroupModule : NancyModule
    {
        private readonly IGroupService groupService;

        private readonly IPermissionService permissionService;

        private readonly IAuthorisationService authorisationService;

        public GroupModule(IGroupService groupService, IPermissionService permissionService, IAuthorisationService authorisationService)
        {
            this.groupService = groupService;
            this.permissionService = permissionService;
            this.authorisationService = authorisationService;
            this.Post("/authorisation/groups", _ => this.CreateGroup());
            this.Get("/authorisation/groups", _ => this.GetGroups());
            this.Get("/authorisation/groups/{id:int}", parameters => this.GetGroup(parameters.id));
            this.Get("/authorisation/groups/{id:int}/permissions", parameters => this.GetGroupPermissions(parameters.id));
            this.Post("/authorisation/groups/{id:int}/members", parameters => this.AddGroupMember(parameters.id));
            this.Put("/authorisation/groups/{id:int}", parameters => this.UpdateGroup(parameters.id));
            this.Delete("/authorisation/groups/{id:int}/members/{memberId:int}", parameters => this.RemoveGroupMember(parameters.id, parameters.memberId));
        }

        private object CreateGroup()
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            if (!this.authorisationService.HasPermissionFor(AuthorisedAction.AuthorisationAdmin, privileges))
            {
                return this.Negotiate.WithModel((new BadRequestResult<string>("You are not authorised to create groups")));
            }

            var resource = this.Bind<GroupResource>();
            var result = this.groupService.Add(resource);
            return this.Negotiate.WithModel(result);
        }

        private object GetGroup(int id)
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            if (!this.authorisationService.HasPermissionFor(AuthorisedAction.AuthorisationAdmin, privileges))
            {
                return this.Negotiate.WithModel((new BadRequestResult<string>("You are not authorised to view groups")));
            }

            var group = this.groupService.GetById(id);
            return this.Negotiate.WithModel(group);
        }

        private object UpdateGroup(int id)
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            if (!this.authorisationService.HasPermissionFor(AuthorisedAction.AuthorisationAdmin, privileges))
            {
                return this.Negotiate.WithModel((new BadRequestResult<string>("You are not authorised to edit groups")));
            }

            var resource = this.Bind<GroupResource>();
            var result = this.groupService.Update(id, resource);
            return this.Negotiate.WithModel(result);
        }

        private object GetGroups()
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            if (!this.authorisationService.HasPermissionFor(AuthorisedAction.AuthorisationAdmin, privileges))
            {
                return this.Negotiate.WithModel((new BadRequestResult<string>("You are not authorised to view groups")));
            }

            var groups = this.groupService.GetAll();
            return this.Negotiate.WithModel(groups);
        }

        private object AddGroupMember(int id)
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            if (!this.authorisationService.HasPermissionFor(AuthorisedAction.AuthorisationAdmin, privileges))
            {
                return this.Negotiate.WithModel((new BadRequestResult<string>("You are not authorised to edit groups")));
            }

            var resource = this.Bind<GroupMemberResource>();
            var result = this.groupService.AddGroupMember(id, resource);
            return this.Negotiate.WithModel(result);
        }

        private object RemoveGroupMember(int id, int memberId)
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            if (!this.authorisationService.HasPermissionFor(AuthorisedAction.AuthorisationAdmin, privileges))
            {
                return this.Negotiate.WithModel((new BadRequestResult<string>("You are not authorised to edit groups")));
            }

            var result = this.groupService.RemoveGroupMember(id, memberId);
            return this.Negotiate.WithModel(result);
        }

        private object GetGroupPermissions(int id)
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            if (!this.authorisationService.HasPermissionFor(AuthorisedAction.AuthorisationAdmin, privileges))
            {
                return this.Negotiate.WithModel((new BadRequestResult<string>("You are not authorised to view groups")));
            }

            var result = this.permissionService.GetImmediatePermissionsForGroup(id);
            return this.Negotiate.WithModel(result);
        }
    }
}
