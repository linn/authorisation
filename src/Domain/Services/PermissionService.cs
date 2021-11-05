namespace Linn.Authorisation.Domain.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Authorisation.Domain.Exceptions;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Common.Persistence;

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
        public IEnumerable<Permission> GetImmediatePermissionsForGroup(int groupId)
        {
            return this.permissionRepository.FilterBy(p => p is GroupPermission && ((GroupPermission)p).GranteeGroup.Id == groupId).OrderBy(p => p.Privilege.Name);
        }

        public IEnumerable<Permission> GetAllPermissionsForPrivilege(int privilegeId)
        {
            return this.permissionRepository.FilterBy(p => p.Privilege.Active && p.Privilege.Id == privilegeId).OrderBy(p => p.Privilege.Name);
        }

        public IEnumerable<Permission> GetAllPermissionsForUser(string who)
        {
            if (string.IsNullOrEmpty(who))
            {
                throw new NoGranteeUriProvidedException("no granteeUri provided");
            }

            var permissions = this.permissionRepository
                .FilterBy(p => p is IndividualPermission && ((IndividualPermission)p).GranteeUri == who).ToList();

            var groups = this.groupRepository.FindAll().ToList();

            if (!groups.Any(g => g.IsMemberOf(who)))
            {
                return permissions.Where(p => p.Privilege.Active).Distinct().OrderBy(p => p.Privilege.Name);
            }

            var groupPermissions = this.permissionRepository.FilterBy(
                p => p is GroupPermission && ((GroupPermission)p).GranteeGroup.IsMemberOf(who));
            permissions.AddRange(groupPermissions);

            return permissions.Where(p => p.Privilege.Active).Distinct().OrderBy(p => p.Privilege.Name);
        }
    }
}
