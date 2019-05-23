namespace Linn.Authorisation.Service.ResponseProcessors
{
    using Linn.Authorisation.Domain;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;

    public class PrivilegeResponseProcessor : JsonResponseProcessor<Privilege>
    {
        public PrivilegeResponseProcessor(IResourceBuilder<Privilege> resourceBuilder)
            : base(resourceBuilder, "privilege", 1)
        {
        }
    }
}