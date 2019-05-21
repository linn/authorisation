namespace Linn.Authorisation.Domain.Permissions
{
    using System;

    public class IndividualPermission : Permission
    {
        public IndividualPermission(string granteeUri, Privilege privilege, string grantedByUri) : base(privilege, grantedByUri)
        {
            this.GranteeUri = granteeUri;
        }

        public string GranteeUri { get; set; }       
    }
}