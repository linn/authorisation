namespace Linn.Authorisation.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    public class PrivilegesResourceBuilder : IResourceBuilder<IEnumerable<Privilege>>
    {
        private readonly PrivilegeResourceBuilder privilegeResourceBuilder = new PrivilegeResourceBuilder();

        public IEnumerable<PrivilegeResource> Build(IEnumerable<Privilege> privileges)
        {
            return privileges.Select(a => this.privilegeResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<Privilege>>.Build(IEnumerable<Privilege> privilege) =>
            this.Build(privilege);

        public string GetLocation(IEnumerable<Privilege> privileges)
        {
            throw new NotImplementedException();
        }
    }
}
