namespace Linn.Authorisation.Domain.Permissions
{
    using System.Collections.Generic;

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

        public bool CheckUnique(IEnumerable<GroupPermission> existingPermissions)
            {
                foreach (var permission in existingPermissions)
                {
                    if (permission.Privilege.Name == this.Privilege.Name && permission.GranteeGroup.Id == this.GranteeGroup.Id)
                    {
                        return false;
                    }
                }

                return true;
            }
    }
}
