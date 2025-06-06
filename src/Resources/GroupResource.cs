﻿namespace Linn.Authorisation.Resources
{
    using System.Collections.Generic;

    using Linn.Common.Resources;

    public class GroupResource : HypermediaResource
    {
        public string Name { get; set; }

        public bool Active { get; set; }

        public int Id { get; set; }

        public IEnumerable<MemberResource> Members { get; set; }

        public IEnumerable<PermissionResource> Permissions { get; set; }
    }
}
