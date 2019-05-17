namespace Linn.Authorisation.Domain.Groups
{
    using System;
    using System.Collections.Generic;
    using Groups;

    public class GroupGroupMember : GroupMember
    {
        public GroupGroupMember(Group group, string addedByUri)
        {
            this.Group = group;
            this.AddedByUri = addedByUri;
            this.DateAdded = DateTime.UtcNow;
        }

        public Group Group { get; set; }

        public override IEnumerable<string> MemberUris()
        {
            return this.Group.MemberUris();
        }
    }
}