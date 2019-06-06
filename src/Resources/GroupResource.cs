namespace Linn.Authorisation.Resources
{
    using System.Collections.Generic;
    using Common.Resources;

    public class GroupResource : HypermediaResource
    {
        public string Name { get; set; }

        public bool Active { get; set; }

        public IEnumerable<GroupMemberResource> Members { get; set; }
    }
}