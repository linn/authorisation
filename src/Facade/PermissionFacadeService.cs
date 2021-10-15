namespace Linn.Authorisation.Facade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Domain.Services;
    using Linn.Authorisation.Facade.Exceptions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Microsoft.EntityFrameworkCore;

    public class PermissionFacadeService : FacadeService<Permission, int, PermissionResource, PermissionResource>, IPermissionFacadeService
    {
        private readonly IRepository<Privilege, int> privilegeRepository;

        private readonly IRepository<Group, int> groupRepository;

        private readonly IRepository<Permission, int> permissionRepository;

        private readonly ITransactionManager transactionManager;

        private readonly IPermissionService permissionService;

        public PermissionFacadeService(IRepository<Permission, int> repository,
                                       ITransactionManager transactionManager,
                                       IRepository<Privilege, int> privilegeRepository,
                                       IRepository<Group, int> groupRepository,
                                       IPermissionService permissionService)
            : base(repository, transactionManager)
        {
            this.privilegeRepository = privilegeRepository;
            this.groupRepository = groupRepository;
            this.permissionRepository = repository;
            this.transactionManager = transactionManager;
            this.permissionService = permissionService;
        }

        public IResult<Permission> CreatePermission(PermissionResource resource)
        {
            if (resource.GranteeUri != null && resource.GroupName != null)
            {
                return new BadRequestResult<Permission>();
            }

            var existingIdenticalPermission = this.permissionRepository.FindBy(
                x => x.Privilege.Name == resource.Privilege
                     && ((x is GroupPermission && ((GroupPermission)x).GranteeGroup.Name == resource.GroupName)
                     || (x is IndividualPermission && ((IndividualPermission)x).GranteeUri == resource.GranteeUri)));

            if (existingIdenticalPermission != null)
            {
                return new BadRequestResult<Permission>("Permission already exists");
            }

            return this.Add(resource);
        }

        public IResult<IEnumerable<Permission>> GetImmediatePermissionsForGroup(int groupId)
        {
            var permissions = this.permissionRepository
                .FilterBy(p => p is GroupPermission && ((GroupPermission)p).GranteeGroup.Id == groupId)
                .Include(x => ((GroupPermission)x).GranteeGroup)
                .Include(x => x.Privilege).ToList();

            return new SuccessResult<IEnumerable<Permission>>(permissions.Distinct());
        }

        public IResult<IEnumerable<Permission>> GetAllPermissionsForPrivilege(int privilegeId)
        {
            var groupPermissions = this.permissionRepository.FilterBy(p => p is GroupPermission && ((GroupPermission)p).Privilege.Id == privilegeId)
                    .Include(x => ((GroupPermission)x).GranteeGroup)
                    .Include(x => x.Privilege).ToList();

            var individualPermissions = this.permissionRepository.FilterBy(p => p is IndividualPermission && ((IndividualPermission)p).Privilege.Id == privilegeId)
                    .Include(x => x.Privilege).ToList();

            var permissions = groupPermissions.Concat(individualPermissions);

            return new SuccessResult<IEnumerable<Permission>>(permissions.Distinct());
        }


        public IResult<IEnumerable<Permission>> GetAllPermissionsForUser(string granteeUri)
        {
            var result = this.permissionService.GetAllPermissionsForUser(granteeUri);

            return new SuccessResult<IEnumerable<Permission>>(result);

        }

        public IResult<Permission> RemovePermission(PermissionResource resource)
        {
            var permission = this.CreateFromResource(resource);

            if ((resource.GranteeUri == null && resource.GroupName == null) || (resource.GranteeUri != null && resource.GroupName != null))
            {
                return new BadRequestResult<Permission>();
            }

            if (permission is IndividualPermission)
            {
                RemoveIndividualPermission(permission);
            }
            else
            {
                RemoveGroupPermission(permission);
            }

            return new SuccessResult<Permission>(permission);
        }

        private void RemoveIndividualPermission(Permission permission)
        {
            var castedIndividualPermission = (IndividualPermission)permission;

            var individualPermission = this.permissionRepository.FilterBy(
                p => p is IndividualPermission
                     && ((IndividualPermission)p).GranteeUri == castedIndividualPermission.GranteeUri
                     && p.Privilege.Id == castedIndividualPermission.Privilege.Id).First();

            this.permissionRepository.Remove(individualPermission);
            this.transactionManager.Commit();
        }

        private void RemoveGroupPermission(Permission permission)
        {
            var castedGroupPermission = (GroupPermission)permission;

            var groupPermission = this.permissionRepository.FilterBy(
                    p => p is GroupPermission
                         && ((GroupPermission)p).GranteeGroup == castedGroupPermission.GranteeGroup
                         && p.Privilege.Id == castedGroupPermission.Privilege.Id).First();

            this.permissionRepository.Remove(groupPermission);
            this.transactionManager.Commit();
        }

        protected override Permission CreateFromResource(PermissionResource resource)
        {
            var privilege = this.privilegeRepository.FilterBy(p => p.Name == resource.Privilege).FirstOrDefault();
            if (privilege == null)
            {
                throw new PrivilegeNotFoundException("Privilege Not Found");
            }

            if (resource.GranteeUri != null)
            {
                return new IndividualPermission(resource.GranteeUri, privilege, resource.GrantedByUri);
            }

            var group = this.groupRepository.FilterBy(g => g.Name == resource.GroupName).FirstOrDefault();
            if (group == null)
            {
                throw new GroupNotFoundException("Group Not Found");
            }
            return new GroupPermission(group, privilege, resource.GrantedByUri);
        }

        protected override void UpdateFromResource(Permission entity, PermissionResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<Permission, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
