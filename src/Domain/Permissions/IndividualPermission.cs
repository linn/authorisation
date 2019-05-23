namespace Linn.Authorisation.Domain.Permissions
{
    using System;

    public class IndividualPermission : Permission
    {
        public IndividualPermission(string granteeUri, Privilege privilege, DateTime dateGranted, string grantedByUri) : base(privilege, dateGranted, grantedByUri)
        {
            this.GranteeUri = granteeUri;
        }

        public IndividualPermission()
        {
            // empty args constructor needed for ef
        }

        public string GranteeUri { get; set; }       
    }
}
