using System;
using System.Collections.Generic;
using System.Linq;

namespace Linn.Authorisation.Domain.Permissions
{
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
            foreach (var permission in existingPermissions.Where(p => p.Privilege.Id == this.Privilege.Id && p.GranteeUri == this.GranteeUri))
            {
                return false;
            }

            return true;
        }
    }
}
