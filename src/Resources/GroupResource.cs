namespace Linn.Authorisation.Resources
{
    using System;
    using System.Collections.Generic;

    public class GroupResource
    {
        public string Name { get; set; }

        public bool Active { get; set; }

        public int Id { get; set; }

        public IEnumerable<MemberResource> Members { get; set; }
    }
}
