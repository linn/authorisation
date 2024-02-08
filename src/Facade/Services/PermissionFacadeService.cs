namespace Linn.Authorisation.Facade.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Domain.Services;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;

    public class PermissionFacadeService : IPermissionFacadeService
    {
        private readonly IPermissionService permissionService;

        private readonly IBuilder<Permission> resourceBuilder;

        private readonly IRepository<Permission, int> permissionsRepository;

        private readonly IRepository<Privilege, int> privilegeRepository;

        private readonly ITransactionManager transactionManager;

        public PermissionFacadeService(
            IPermissionService permissionService,
            IBuilder<Permission> resourceBuilder,
            IRepository<Permission, int> permissionRepository,
            IRepository<Privilege,int> privilegeRepository,
            ITransactionManager transactionManager)
        {
            this.permissionService = permissionService;
            this.resourceBuilder = resourceBuilder;
            this.permissionsRepository = permissionRepository;
            this.privilegeRepository = privilegeRepository;
            this.transactionManager = transactionManager;
        }

        public IResult<PermissionResource> CreatePermission(PermissionResource permissionResource)
        {
            var privilege = this.privilegeRepository.FindById(permissionResource.PrivilegeId);

            var permission = new IndividualPermission(
                permissionResource.GranteeUri, privilege, permissionResource.GrantedByUri);

            this.permissionsRepository.Add(permission);

            this.transactionManager.Commit();

            var result = new PermissionResource
            {
                DateGranted = permission.DateGranted.ToString("o"),
                GrantedByUri = permission.GrantedByUri,
                GranteeUri = permission.GranteeUri,
                PrivilegeId = permission.Privilege.Id
            };

            return new SuccessResult<PermissionResource>(result);
        }

        public IResult<IEnumerable<PermissionResource>> GetAllPermissionsForUser(string granteeUri)
        {
            var result = this.permissionService.GetAllPermissionsForUser(granteeUri);

            var resources = result.Select(x => (PermissionResource)this.resourceBuilder.Build(x, null));

            return new SuccessResult<IEnumerable<PermissionResource>>(resources);
        }
    }
}
