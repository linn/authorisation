namespace Linn.Authorisation.Service.Modules
{
    using System.Linq;
    using Linn.Authorisation.Facade;
    using Linn.Authorisation.Resources;
    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Service.Extensions;
    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Security;

    public sealed class PrivilegesModule : NancyModule
    {
        private readonly IPrivilegeFacadeService privilegeService;

        private readonly IAuthorisationService authorisationService;

        public PrivilegesModule(IPrivilegeFacadeService privilegeService, IAuthorisationService authorisationService)
        {
            this.privilegeService = privilegeService;
            this.authorisationService = authorisationService;

            this.Get("/authorisation/privileges/all", _ => this.GetPrivileges());
            this.Post("/authorisation/privileges", _ => this.CreatePrivilege());
            this.Get("/authorisation/privileges/{id:int}", parameters => this.GetPrivilege(parameters.id));
            this.Put("/authorisation/privileges/{id:int}", parameters => this.UpdatePrivilege(parameters.id));
            this.Delete("/authorisation/privileges/{id:int}", parameters => this.RemovePrivilege(parameters.id));
        }

        private object GetPrivilege(int id)
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            if (!this.authorisationService.HasPermissionFor(AuthorisedAction.AuthorisationAdmin, privileges))
            {
                return this.Negotiate.WithModel((new BadRequestResult<string>("You are not authorised to view privileges")));
            }

            var privilege = this.privilegeService.GetById(id);
            return this.Negotiate.WithModel(privilege);
        }

        private object UpdatePrivilege(int id)
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            if (!this.authorisationService.HasPermissionFor(AuthorisedAction.AuthorisationAdmin, privileges))
            {
                return this.Negotiate.WithModel((new BadRequestResult<string>("You are not authorised to edit privileges")));
            }

            var resource = this.Bind<PrivilegeResource>();
            var result = this.privilegeService.Update(id, resource);
            return this.Negotiate.WithModel(result);
        }

        private object CreatePrivilege()
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            if (!this.authorisationService.HasPermissionFor(AuthorisedAction.AuthorisationAdmin, privileges))
            {
                return this.Negotiate.WithModel((new BadRequestResult<string>("You are not authorised to create privileges")));
            }

            var resource = this.Bind<PrivilegeResource>();
            var result = this.privilegeService.Add(resource);
            return this.Negotiate.WithModel(result);
        }

        private object RemovePrivilege(int id)
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            if (!this.authorisationService.HasPermissionFor(AuthorisedAction.AuthorisationAdmin, privileges))
            {
                return this.Negotiate.WithModel((new BadRequestResult<string>("You are not authorised to remove privileges")));
            }

            var result = this.privilegeService.Remove(id);
            return this.Negotiate.WithModel(result);
        }

        private object GetPrivileges()
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            if (!this.authorisationService.HasPermissionFor(AuthorisedAction.AuthorisationAdmin, privileges))
            {
                return this.Negotiate.WithModel((new BadRequestResult<string>("You are not authorised to view privileges")));
            }

            var privilege = this.privilegeService.GetAll();
            return this.Negotiate.WithModel(privilege);
        }
    }
}
