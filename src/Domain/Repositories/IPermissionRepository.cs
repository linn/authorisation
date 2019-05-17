namespace Linn.Authorisation.Domain.Repositories
{
    using System.Collections.Generic;
    using Groups;
    using Permissions;

    public interface IPermissionRepository
    {
        IEnumerable<Permission> GetIndividualPermissions(string who);

        IEnumerable<Permission> GetGroupsPermissions(IEnumerable<Group> groups);
    }
}
