namespace Linn.Authorisation.Domain.Permissions
{
    using System;
    using Groups;
    using Permissions;

    public class GroupPermission : Permission
    {
        public GroupPermission(Group granteeGroup, Privilege privilege, DateTime dateGranted, string grantedByUri) : base(privilege, dateGranted, grantedByUri)
        {
            this.GranteeGroup = granteeGroup;
        }

        public Group GranteeGroup { get; set; }
    }
}