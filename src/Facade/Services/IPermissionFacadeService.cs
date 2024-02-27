namespace Linn.Authorisation.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    public interface IPermissionFacadeService
    {
        IResult<IEnumerable<PermissionResource>> GetAllPermissionsForUser(string granteeUri);

        IResult<PermissionResource> CreateIndividualPermission(PermissionResource permissionResource, string employeeUri);
    }
}
