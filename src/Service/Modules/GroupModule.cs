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
            this.Get("/authorisation/groups/{id:int}", parameters => this.GetGroup(parameters.id));
            this.Post("/authorisation/groups/{id:int}/members", parameters => this.AddGroupMember(parameters.id));
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

        private object AddGroupMember(int id)
        {
            var resource = this.Bind<GroupMemberResource>();
            var result = this.groupService.AddGroupMember(id, resource);
            return this.Negotiate.WithModel(result);
        }
    }
}