namespace Linn.Authorisation.Facade
{
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using System.Collections.Generic;

    public interface IPermissionFacadeService : IFacadeService<Permission, int, PermissionResource, PermissionResource>
    {
        IResult<Permission> CreatePermission(PermissionResource permission);

        IResult<IEnumerable<Permission>> GetImmediatePermissionsForGroup(int groupId);

        IResult<IEnumerable<Permission>> GetAllPermissionsForPrivilege(int privilegeId);

        IResult<Permission> RemovePermission(PermissionResource permission);

        IResult<IEnumerable<Permission>> GetAllPermissionsForUser(string granteeUri);

    }
}
