namespace Linn.Authorisation.Service.Tests.Facade.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Domain.Groups;
    using Domain.Permissions;
    using Domain.Repositories;

    public class TestPermissionRepository : IPermissionRepository
    {
        public IEnumerable<Permission> GetIndividualPermissions(string who)
        {
            return TestDbContext.Permissions.Where(p => p is IndividualPermission && (((IndividualPermission) p).GranteeUri == who));
        }

        public IEnumerable<Permission> GetGroupsPermissions(IEnumerable<Group> groups)
        {
            return TestDbContext.Permissions.Where(p =>
                p is GroupPermission && groups.Contains(((GroupPermission) p).GranteeGroup));
        }
    }
}
