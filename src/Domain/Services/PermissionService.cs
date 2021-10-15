namespace Linn.Authorisation.Domain.Services
{
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Common.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PermissionService : IPermissionService
    {
        private readonly IRepository<Group, int> groupRepository;

        private readonly IRepository<Permission, int> permissionRepository;

        public PermissionService(
            IRepository<Group, int> groupRepository,
            IRepository<Permission, int> permissionRepository)
        {
            this.groupRepository = groupRepository;
            this.permissionRepository = permissionRepository;
        }

        public IEnumerable<Permission> GetAllPermissionsForUser(string who)
        {
            if (who == String.Empty)
            {
                return null;
            }

            var permissions = this.permissionRepository
                .FilterBy(p => p is IndividualPermission && ((IndividualPermission)p).GranteeUri == who).ToList();

            var groups = this.groupRepository.FindAll().ToList();

            if (!groups.Any(g => g.IsMemberOf(who)))
            {
                return permissions.Where(p => p.Privilege.Active).Distinct();
            }

            var groupPermissions = this.permissionRepository.FilterBy(
                p => p is GroupPermission && ((GroupPermission)p).GranteeGroup.IsMemberOf(who));
            permissions.AddRange(groupPermissions);

            return permissions.Where(p => p.Privilege.Active).Distinct();
        }
    }

}