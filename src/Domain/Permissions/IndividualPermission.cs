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
    }
}
