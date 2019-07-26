namespace Linn.Authorisation.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Authorisation.Domain.Permissions;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;

    public class PermissionsResponseProcessor : JsonResponseProcessor<IEnumerable<Permission>>
    {
        public PermissionsResponseProcessor(IResourceBuilder<IEnumerable<Permission>> resourceBuilder)
            : base(resourceBuilder, "permissions", 1)
        {
        }
    }
}