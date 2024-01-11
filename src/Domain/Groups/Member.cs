namespace Linn.Authorisation.Domain.Groups
{
    using System;
    using System.Collections.Generic;

    public abstract class Member : Entity
    {
        public DateTime DateAdded { get; set; }

        public string AddedByUri { get; set; }

        public abstract IEnumerable<string> MemberUris();
    }
}