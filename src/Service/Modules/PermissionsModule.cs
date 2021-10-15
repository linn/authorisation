namespace Linn.Authorisation.Service.Modules
{
    using System;
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

    public sealed class PermissionsModule : NancyModule
    {
        private readonly IPermissionFacadeService permissionService;
        private readonly IAuthorisationService authorisationService;

        public PermissionsModule(IPermissionFacadeService permissionService, IAuthorisationService authorisationService)
        {
            this.permissionService = permissionService;
            this.authorisationService = authorisationService;
            this.Post("/authorisation/permissions", _ => this.CreatePermission());
            this.Delete("/authorisation/permissions", _ => this.RemovePermission());
            this.Get("/authorisation/permissions/{id:int}", parameters => this.GetPermissionsForPrivilege(parameters.id));
            this.Get("/authorisation/permissions/user", parameters => this.GetAllPermissionsForUser());
        }

        private object CreatePermission()
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            if (!this.authorisationService.HasPermissionFor(AuthorisedAction.AuthorisationAdmin, privileges))
            {
                return this.Negotiate.WithModel((new BadRequestResult<string>("You are not authorised to create permissions")));
            }

            var resource = this.Bind<PermissionResource>();
            var result = this.permissionService.CreatePermission(resource);
            return this.Negotiate.WithModel(result);
        }

        private object RemovePermission()
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            if (!this.authorisationService.HasPermissionFor(AuthorisedAction.AuthorisationAdmin, privileges))
            {
                return this.Negotiate.WithModel((new BadRequestResult<string>("You are not authorised to remove permissions")));
            }

            var resource = this.Bind<PermissionResource>();
            var result = this.permissionService.RemovePermission(resource);
            return this.Negotiate.WithModel(result);
        }

        private object GetPermissionsForPrivilege(int privilegeId)
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            if (!this.authorisationService.HasPermissionFor(AuthorisedAction.AuthorisationAdmin, privileges))
            {
                return this.Negotiate.WithModel((new BadRequestResult<string>("You are not authorised to view permissions")));
            }

            var resource = this.Bind<PermissionResource>();
            var result = this.permissionService.GetAllPermissionsForPrivilege(privilegeId);
            return this.Negotiate.WithModel(result);
        }

        private object GetAllPermissionsForUser()
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            if (!this.authorisationService.HasPermissionFor(AuthorisedAction.AuthorisationAdmin, privileges))
            {
                return this.Negotiate.WithModel((new BadRequestResult<string>("You are not authorised to remove permissions")));
            }

            var resource = this.Bind<PermissionResource>();
            var result = this.permissionService.GetAllPermissionsForUser(this.Bind<IndividalPermissionRequestResource>().GranteeUri);
            return this.Negotiate.WithModel(result);
        }
    }
}
