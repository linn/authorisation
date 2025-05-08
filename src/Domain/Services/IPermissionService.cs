namespace Linn.Authorisation.Domain.Services
{
    using System.Collections.Generic;

    using Linn.Authorisation.Domain.Permissions;

    public interface IPermissionService
    {
        IEnumerable<Permission> GetImmediatePermissionsForGroup(int groupId);

        IEnumerable<Permission> GetAllPermissionsForPrivilege(int privilegeId, IEnumerable<string> userPrivileges = null);

        IEnumerable<Permission> GetAllPermissionsForUser(string who, IEnumerable<string> userPrivileges = null);

        IList<string> GetAllGranteeUris(IEnumerable<Permission> permissions);
    }
}
