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
        private readonly GroupResourceBuilder privilegeResourceBuilder = new GroupResourceBuilder();

        public IEnumerable<GroupResource> Build(IEnumerable<Group> privileges)
        {
            return privileges.Select(a => this.privilegeResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<Group>>.Build(IEnumerable<Group> privilege) =>
            this.Build(privilege);

        public string GetLocation(IEnumerable<Group> privileges)
        {
            throw new NotImplementedException();
        }
    }
}
