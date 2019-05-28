namespace Linn.Authorisation.Domain.Permissions
{
    using System;
    using Groups;

    public class GroupPermission : Permission
    {
        public GroupPermission(Group granteeGroup, Privilege privilege, string grantedByUri) : base(privilege, grantedByUri)
        {
            this.GranteeGroup = granteeGroup;
        }

        public GroupPermission()
        {
            // empty args constructor needed for ef
        }

        public Group GranteeGroup { get; set; }
    }
}
