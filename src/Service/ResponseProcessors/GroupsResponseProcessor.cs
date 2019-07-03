namespace Linn.Authorisation.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;

    public class GroupsResponseProcessor : JsonResponseProcessor<IEnumerable<Group>>
    {
        public GroupsResponseProcessor(IResourceBuilder<IEnumerable<Group>> resourceBuilder)
            : base(resourceBuilder, "groups", 1)
        {
        }
    }
}