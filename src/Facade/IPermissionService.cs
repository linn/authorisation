namespace Linn.Authorisation.Facade
{
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    public interface IPermissionService : IFacadeService<Permission, int, PermissionResource, PermissionResource>
    {
        IResult<Permission> CreatePermission(PermissionResource permission);
    }
}