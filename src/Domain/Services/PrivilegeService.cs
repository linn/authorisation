namespace Linn.Authorisation.Domain.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Authorisation.Domain.Exceptions;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Common.Persistence;

    public class PrivilegeService : IPrivilegeService
    {
        private readonly IRepository<Group, int> groupRepository;

        private readonly IRepository<Permission, int> permissionRepository;

        public PrivilegeService(
            IRepository<Group, int> groupRepository,
            IRepository<Permission, int> permissionRepository)
        {
            this.groupRepository = groupRepository;
            this.permissionRepository = permissionRepository;
        }

        public IEnumerable<Privilege> GetPrivileges(string who)
        {
            if (who == string.Empty)
            {
                throw new NoGranteeUriProvidedException("no granteeUri provided");
            }

            var privileges = this.permissionRepository
                .FilterBy(p => p is IndividualPermission && ((IndividualPermission)p).GranteeUri == who)
                .Select(p => p.Privilege).ToList();

            var groups = this.groupRepository.FindAll().Where(x => x.Active).ToList();

            if (!groups.Any(g => g.IsMemberOf(who)))
            {
                return privileges.Where(p => p.Active).Distinct();
            }

            var groupPermissions = this.permissionRepository.FilterBy(
                p => p is GroupPermission && ((GroupPermission)p).GranteeGroup.IsMemberOf(who));
            privileges.AddRange(groupPermissions.Select(p => p.Privilege));

            return privileges.Where(p => p.Active).Distinct();
        }
    }

}