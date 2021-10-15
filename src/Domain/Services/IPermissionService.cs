namespace Linn.Authorisation.Domain.Services
{
    using System.Collections.Generic;

    using Linn.Authorisation.Domain.Permissions;

    public interface IPermissionService
    {
        IEnumerable<Permission> GetAllPermissionsForUser(string who);
    }
}