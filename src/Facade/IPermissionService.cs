namespace Linn.Authorisation.Facade
{
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    public interface IPermissionService : IFacadeService<Permission, int, PermissionCreateResource, PermissionResource>
    {
        IResult<Permission> CreatePermission(PermissionCreateResource permission);
    }
}