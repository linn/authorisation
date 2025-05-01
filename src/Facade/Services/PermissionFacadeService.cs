namespace Linn.Authorisation.Facade.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
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

        private readonly IRepository<Group, int> groupRepository;

        private readonly ITransactionManager transactionManager;

        public PermissionFacadeService(
            IPermissionService permissionService,
            IBuilder<Permission> resourceBuilder,
            IRepository<Permission, int> permissionRepository,
            IRepository<Privilege, int> privilegeRepository,
            IRepository<Group, int> groupRepository,
            ITransactionManager transactionManager)
        {
            this.permissionService = permissionService;
            this.resourceBuilder = resourceBuilder;
            this.permissionsRepository = permissionRepository;
            this.privilegeRepository = privilegeRepository;
            this.transactionManager = transactionManager;
            this.groupRepository = groupRepository;
        }

        public IResult<IList<string>> GetPermissionsForPrivilege(int privilegeId, IEnumerable<string> privileges = null)
        {
            var permissions = this.permissionService.GetAllPermissionsForPrivilege(privilegeId);

            var result = this.permissionService.GetAllGranteeUris(permissions);

            return new SuccessResult<IList<string>>(result);
        }

        public IResult<IEnumerable<PermissionResource>> GetAllPermissionsForGroup(int groupId, IEnumerable<string> privileges = null)
        {
            var permissions = this.permissionService.GetImmediatePermissionsForGroup(groupId);

            var result = new List<PermissionResource>();

            foreach (var permission in permissions)
            {
                result.Add(new PermissionResource
                    {
                        GroupName = ((GroupPermission)permission).GranteeGroup.Name,
                        PrivilegeId = permission.Privilege.Id,
                        Privilege = permission.Privilege.Name,
                    });
            }

            return new SuccessResult<IEnumerable<PermissionResource>>(result);
        }

        public IResult<PermissionResource> CreateIndividualPermission(
            PermissionResource permissionResource,
            string employeeUri)
        {
            var privilege = this.privilegeRepository.FindById(permissionResource.PrivilegeId);

            var permission = new IndividualPermission(
                permissionResource.GranteeUri,
                privilege,
                permissionResource.GrantedByUri);

            var permissions = this.permissionsRepository.FilterBy(p => p is IndividualPermission);

            var individualPermissions = permissions.Select(p => (IndividualPermission)p);

            if (permission.CheckUnique(individualPermissions))
            {
                this.permissionsRepository.Add(permission);

                this.transactionManager.Commit();

                var result = new PermissionResource
                                 {
                                     DateGranted = permission.DateGranted.ToString("o"),
                                     GrantedByUri = employeeUri,
                                     GranteeUri = permission.GranteeUri,
                                     PrivilegeId = permission.Privilege.Id
                                 };

                return new CreatedResult<PermissionResource>(result);
            }

            return new BadRequestResult<PermissionResource>("Grantee already has privilege");
        }

        public IResult<PermissionResource> CreateGroupPermission(
            PermissionResource permissionResource,
            string employeeUri)
        {
            var privilege = this.privilegeRepository.FindById(permissionResource.PrivilegeId);

            if (permissionResource.GranteeGroupId != null)
            {
                var group = this.groupRepository.FindById((int)permissionResource.GranteeGroupId);

                var permission = new GroupPermission(group, privilege, permissionResource.GrantedByUri);

                var permissions = this.permissionsRepository.FilterBy(p => p is GroupPermission);

                var groupPermissions = permissions.Select(p => (GroupPermission)p);

                if (permission.CheckUnique(groupPermissions))
                {
                    this.permissionsRepository.Add(permission);
                    this.transactionManager.Commit();

                    var result = new PermissionResource
                                     {
                                         DateGranted = permission.DateGranted.ToString("o"),
                                         GrantedByUri = employeeUri,
                                         PrivilegeId = permission.Privilege.Id,
                                         GranteeGroupId = permission.GranteeGroup.Id,
                                         GroupName = permission.GranteeGroup.Name,
                                     };
                    return new CreatedResult<PermissionResource>(result);
                }
            }

            return new BadRequestResult<PermissionResource>("Grantee already has privilege");
        }

        public IResult<PermissionResource> DeletePermission(int permissionId)
        {
            var permission = this.permissionsRepository.FindById(permissionId);

            if (permission == null)
            {
                return new BadRequestResult<PermissionResource>("Unable to remove Permission");
            }

            this.permissionsRepository.Remove(permission);

            this.transactionManager.Commit();

            var result = new PermissionResource { Id = permission.Id, };

            return new SuccessResult<PermissionResource>(result);
        }

        public IResult<IEnumerable<PermissionResource>> GetAllPermissionsForUser(string granteeUri, IEnumerable<string> privileges = null)
        {
            var result = this.permissionService.GetAllPermissionsForUser(granteeUri);
            var resources = result.Select(x => (PermissionResource)this.resourceBuilder.Build(x, privileges));

            return new SuccessResult<IEnumerable<PermissionResource>>(resources);
        }
    }
}
