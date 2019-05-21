﻿namespace Linn.Authorisation.Domain.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Authorisation.Domain.Repositories;

    public class PrivilegeService : IPrivilegeService
    {
        private readonly IGroupRepository groupRepository;
        private readonly IPermissionRepository permissionRepository;

        public PrivilegeService(IGroupRepository groupRepository, IPermissionRepository permissionRepository)
        {
            this.groupRepository = groupRepository;
            this.permissionRepository = permissionRepository;
        }

        public IEnumerable<Privilege> GetPrivileges(string who)
        {
            var privileges = this.permissionRepository.GetIndividualPermissions(who)
                .Select(p => p.Privilege).ToList();

            var groups = this.groupRepository.GetGroups().Where(g => g.IsMemberOf(who));
            if (!groups.Any())
            {
                return privileges.Where(p => p.Active).Distinct();
            }

            var groupPermissions = this.permissionRepository.GetGroupsPermissions(groups);
            privileges.AddRange(groupPermissions.Select(p => p.Privilege));

            return privileges.Where(p => p.Active).Distinct();
        }
    }
}