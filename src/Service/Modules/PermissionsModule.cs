namespace Linn.Authorisation.Service
{
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Facade;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    using Nancy;
    using Nancy.ModelBinding;

    public class PermissionsModule : NancyModule
    {
        private readonly IPermissionService permissionService;

        public PermissionsModule(IPermissionService permissionService)
        {
            this.permissionService = permissionService;

            this.Post("/authorisation/permissions", _ => this.CreatePermission());
        }

        private object CreatePermission()
        {
            var resource = this.Bind<PermissionResource>();
            var result = this.permissionService.CreatePermission(resource);
            return this.Negotiate.WithModel(result);
        }
    }
}