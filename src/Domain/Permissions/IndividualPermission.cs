namespace Linn.Authorisation.Domain.Permissions
{
    using System.Collections.Generic;
    using System.Linq;

    public class IndividualPermission : Permission
    {
        public IndividualPermission(string granteeUri, Privilege privilege, string grantedByUri) : base(privilege, grantedByUri)
        {
            this.GranteeUri = granteeUri;
        }

        public IndividualPermission()
        {
            // empty args constructor needed for ef
        }

        public string GranteeUri { get; set; }

        public bool CheckUnique(IEnumerable<IndividualPermission> existingPermissions)
        { 
            bool isUnique = true;
      
            foreach (var permission in existingPermissions)
            {
                if (permission.Privilege.Name == this.Privilege.Name && permission.GranteeUri == this.GranteeUri)
                { 
                    isUnique = false; 
                }
            }

            return isUnique;
        }
    }
}
