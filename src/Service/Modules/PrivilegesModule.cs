namespace Linn.Authorisation.Service.Modules
{
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class PrivilegesModule : NancyModule
    {
        private readonly IFacadeService<Privilege, int, PrivilegeResource, PrivilegeResource> privilegeService;

        public PrivilegesModule(IFacadeService<Privilege, int, PrivilegeResource, PrivilegeResource> privilegeService)
        {
            this.privilegeService = privilegeService;

            this.Post("/authorisation/privileges", _ => this.CreatePrivilege());
            this.Get("/authorisation/privileges", _ => this.GetPrivileges());
            this.Get("/authorisation/privileges/{id:int}", parameters => this.GetPrivilege(parameters.id));
        }

        private object CreatePrivilege()
        {
            var resource = this.Bind<PrivilegeResource>();
            var result = this.privilegeService.Add(resource);
            return this.Negotiate.WithModel(result);
        }

        private object GetPrivileges()
        {
            var privileges = this.privilegeService.GetAll();
            return this.Negotiate.WithModel(privileges);
        }

        private object GetPrivilege(int id)
        {
            var privilege = this.privilegeService.GetById(id);
            return this.Negotiate.WithModel(privilege);
        }
    }
}
