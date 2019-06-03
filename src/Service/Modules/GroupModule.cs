namespace Linn.Authorisation.Service.Modules
{
    using Common.Facade;
    using Domain.Groups;
    using Nancy;
    using Nancy.ModelBinding;
    using Resources;

    public sealed class GroupModule : NancyModule
    {
        private readonly IFacadeService<Group, int, GroupResource, GroupResource> groupService;

        public GroupModule(IFacadeService<Group, int, GroupResource, GroupResource> groupService)
        {
            this.groupService = groupService;
            this.Post("/authorisation/groups", _ => this.CreateGroup());
            this.Get("/authorisation/groups/{id:int}", parameters => this.GetGroup(parameters.id));
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
    }
}