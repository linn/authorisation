namespace Linn.Authorisation.Domain.Services
{
    using System.Collections.Generic;
    using System.Linq;
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
            return this.permissionRepository.FilterBy(
                p => p is GroupPermission && ((GroupPermission)p).GranteeGroup.Id == groupId).OrderBy(p => p.Privilege.Name);
        }

        public IEnumerable<Permission> GetAllPermissionsForPrivilege(int privilegeId, IEnumerable<string> userPrivileges = null)
        {
            if (userPrivileges.Contains(AuthorisedAction.AuthorisationAuthManager))
            {
                return this.permissionRepository.FilterBy(p => p.Privilege.Active && p.Privilege.Id == privilegeId).OrderBy(p => p.Privilege.Name);
            }

            if (userPrivileges == null || !userPrivileges.Any())
            {
                return new List<Permission>();
            }

            var privilegesUserCanManage = userPrivileges
                .Where(p => p.ToLower().Contains("auth-manager"))
                .Select(p => p.Split('.')[0])
                .Distinct()
                .ToList();

            if (!privilegesUserCanManage.Any())
            {
                return new List<Permission>();
            }

            var resultList = this.permissionRepository
                .FindAll()
                .AsEnumerable()
                .Where(p => privilegesUserCanManage.Contains(
                                p.Privilege.Name.Split('.')[0]) && p.Privilege.Active && p.Privilege.Id == privilegeId).OrderBy(p => p.Privilege.Name)
                .ToList();

            return resultList;
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
