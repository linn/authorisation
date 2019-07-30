namespace Linn.Authorisation.Facade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Facade.Exceptions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;

    using Microsoft.EntityFrameworkCore;

    public class PermissionService : FacadeService<Permission, int, PermissionResource, PermissionResource>, IPermissionService
    {
        private readonly IRepository<Privilege, int> privilegeRepository;

        private readonly IRepository<Group, int> groupRepository;

        private readonly IRepository<Permission, int> permissionRepository;

        private readonly ITransactionManager transactionManager;

        public PermissionService(IRepository<Permission, int> repository, ITransactionManager transactionManager, IRepository<Privilege, int> privilegeRepository, IRepository<Group, int> groupRepository)
            : base(repository, transactionManager)
        {
            this.privilegeRepository = privilegeRepository;
            this.groupRepository = groupRepository;
            this.permissionRepository = repository;
            this.transactionManager = transactionManager;
        }

        public IResult<Permission> CreatePermission(PermissionResource resource)
        {
            if (resource.GranteeUri != null && resource.GroupName != null)
            {
                return new BadRequestResult<Permission>();
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

        //public IEnumerable<Privilege> GetAllPrivilegesForGroup(int groupId)
        //{
        //    var privileges = this.permissionRepository
        //        .FilterBy(p => p is GroupPermission && ((GroupPermission)p).GranteeGroup.Id == groupId)
        //        .Select(p => p.Privilege).ToList();

        //    var groups = this.groupRepository.FindAll().Where(g => g.IsMemberOf($"/groups/{groupId}"));
        //    if (!groups.Any())
        //    {
        //        return privileges.Where(p => p.Active).Distinct();
        //    }

        //    var groupPermissions = this.permissionRepository.FilterBy(
        //        p => p is GroupPermission && ((GroupPermission)p).GranteeGroup.IsMemberOf($"/groups/{groupId}"));
        //    privileges.AddRange(groupPermissions.Select(p => p.Privilege));

        //    return privileges.Where(p => p.Active).Distinct();
        //}

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
                     && ((IndividualPermission)p).GranteeUri == castedIndividualPermission.GranteeUri).First();

            this.permissionRepository.Remove(individualPermission);
            this.transactionManager.Commit();
        }

        private void RemoveGroupPermission(Permission permission)
        {
            var castedGroupPermission = (GroupPermission)permission;

            var groupPermission = this.permissionRepository.FilterBy(
                    p => p is GroupPermission
                         && ((GroupPermission)p).GranteeGroup == castedGroupPermission.GranteeGroup)
                .First();

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