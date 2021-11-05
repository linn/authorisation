namespace Linn.Authorisation.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    public class GroupsResourceBuilder : IResourceBuilder<IEnumerable<Group>>
    {
        private readonly GroupResourceBuilder groupResourceBuilder = new GroupResourceBuilder();

        public IEnumerable<GroupResource> Build(IEnumerable<Group> groups)
        {
            return groups.Select(a => this.groupResourceBuilder.Build(a)).OrderBy(a => a.Name);
        }

        object IResourceBuilder<IEnumerable<Group>>.Build(IEnumerable<Group> groups) =>
            this.Build(groups);

        public string GetLocation(IEnumerable<Group> groups)
        {
            throw new NotImplementedException();
        }
    }
}
