namespace Linn.Authorisation.Service.Modules
{
    using Common.Facade;
    using Domain.Groups;
    using Facade;
    using Nancy;
    using Nancy.ModelBinding;
    using Resources;

    public sealed class GroupModule : NancyModule
    {
        private readonly IGroupService groupService;

        public GroupModule(IGroupService groupService)
        {
            this.groupService = groupService;
            this.Post("/authorisation/groups", _ => this.CreateGroup());
            this.Get("/authorisation/groups", _ => this.GetGroups());
            this.Get("/authorisation/groups/{id:int}", parameters => this.GetGroup(parameters.id));
            this.Post("/authorisation/groups/{id:int}/members", parameters => this.AddGroupMember(parameters.id));
            this.Put("/authorisation/groups/{id:int}", parameters => this.UpdateGroup(parameters.id));
            this.Delete("/authorisation/groups/{id:int}/members/{memberId:int}", parameters => this.RemoveGroupMember(parameters.id, parameters.memberId));
        }

        private object CreateGroup()
        {
            var resource = this.Bind<GroupResource>();
            var result = this.groupService.Add(resource);
            return this.Negotiate.WithModel(result);
        }

        private object GetGroup(int id)
        {
            var group = this.groupService.GetById(id);
            return this.Negotiate.WithModel(group);
        }

        private object UpdateGroup(int id)
        {
            var resource = this.Bind<GroupResource>();
            var result = this.groupService.Update(id, resource);
            return this.Negotiate.WithModel(result);
        }

        private object GetGroups()
        {
            var groups = this.groupService.GetAll();
            return this.Negotiate.WithModel(groups);
        }
        
        private object AddGroupMember(int id)
        {
            var resource = this.Bind<GroupMemberResource>();
            var result = this.groupService.AddGroupMember(id, resource);
            return this.Negotiate.WithModel(result);
        }

        private object RemoveGroupMember(int id, int memberId)
        {
            var result = this.groupService.RemoveGroupMember(id, memberId);
            return this.Negotiate.WithModel(result);
        }
    }
}