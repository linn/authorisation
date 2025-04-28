namespace Linn.Authorisation.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    public interface IPermissionFacadeService
    {
        IResult<IEnumerable<PermissionResource>> GetAllPermissionsForUser(string granteeUri, IEnumerable<string> privileges = null);

        IResult<IEnumerable<PermissionResource>> GetPermissionsForPrivilege(int privilegeId, IEnumerable<string> privileges = null);

        IResult<PermissionResource> CreateIndividualPermission(PermissionResource permissionResource, string employeeUri);

        IResult<PermissionResource> CreateGroupPermission(PermissionResource permissionResource, string employeeUri);

        IResult<PermissionResource> DeletePermission(int permissionId);
    }
}