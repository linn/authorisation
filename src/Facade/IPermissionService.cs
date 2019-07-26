namespace Linn.Authorisation.Facade
{
    using System.Collections.Generic;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    public interface IPermissionService : IFacadeService<Permission, int, PermissionResource, PermissionResource>
    {
        IResult<Permission> CreatePermission(PermissionResource permission);
        IResult<IEnumerable<Permission>> GetImmediatePermissionsForGroup(int groupId);

       // IEnumerable<Privilege> GetAllPrivilegesForGroup(int groupId);

        IResult<Permission> RemovePermission(PermissionResource permission);
    }
}