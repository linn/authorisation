namespace Linn.Authorisation.Domain.Groups
{
    using System;
    using System.Collections.Generic;

    public class GroupMember : Member
    {
        public GroupMember(Group group, string addedByUri)
        {
            this.Group = group;
            this.AddedByUri = addedByUri;
            this.DateAdded = DateTime.UtcNow;
        }

        public GroupMember()
        {
            // empty args constructor needed for ef
        }

        public Group Group { get; set; }

        public override IEnumerable<string> MemberUris()
        {
            return this.Group.MemberUris();
        }
    }
}
