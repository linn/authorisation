namespace Linn.Authorisation.Facade.Extensions
{
    using System.Collections.Generic;

    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Facade.ResourceBuilders;
    using Linn.Authorisation.Resources;

    public static class PermissionExtension
    {
        public static PermissionResource ToResource(this Permission permission)
        {
            return new PermissionResourceBuilder().Build(permission);
        }
    }
}
