namespace Linn.Authorisation.Facade.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Domain.Services;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    public class PermissionFacadeService : IPermissionFacadeService
    {
        private readonly IPermissionService permissionService;

        private readonly IBuilder<Permission> resourceBuilder;

        public PermissionFacadeService(
            IPermissionService permissionService,
            IBuilder<Permission> resourceBuilder)
        {
            this.permissionService = permissionService;
            this.resourceBuilder = resourceBuilder;
        }

        public IResult<IEnumerable<PermissionResource>> GetAllPermissionsForUser(string granteeUri)
        {
            var result = this.permissionService.GetAllPermissionsForUser(granteeUri);

            var resources = result.Select(x => (PermissionResource)this.resourceBuilder.Build(x, null));

            return new SuccessResult<IEnumerable<PermissionResource>>(resources);
        }
    }
}
