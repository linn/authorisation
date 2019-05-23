namespace Linn.Authorisation.Service.ResponseProcessors
{
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;

    public class PermissionResponseProcessor : JsonResponseProcessor<Permission>
    {
        public PermissionResponseProcessor(IResourceBuilder<Permission> resourceBuilder)
            : base(resourceBuilder, "permission", 1)
        {
        }
    }
}