namespace Linn.Authorisation.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Authorisation.Domain;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;

    public class PrivilegesResponseProcessor : JsonResponseProcessor<IEnumerable<Privilege>>
    {
        public PrivilegesResponseProcessor(IResourceBuilder<IEnumerable<Privilege>> resourceBuilder)
            : base(resourceBuilder, "privileges", 1)
        {
        }
    }
}