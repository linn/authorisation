namespace Linn.Authorisation.Domain.Services
{
    using System.Collections.Generic;

    using Linn.Authorisation.Domain.Permissions;

    public interface IPermissionService
    {
        IEnumerable<Permission> GetImmediatePermissionsForGroup(int groupId);

        IEnumerable<Permission> GetAllPermissionsForPrivilege(int privilegeId);

        IEnumerable<Permission> GetAllPermissionsForUser(string who);
    }
}
