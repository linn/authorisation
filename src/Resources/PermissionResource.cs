namespace Linn.Authorisation.Resources
{
    using Linn.Common.Resources;

    public class PermissionResource : HypermediaResource
    {
        public string Privilege { get; set; }

        public string GrantedByUri { get; set; }

        public string GranteeUri { get; set; }

        public string GroupName { get; set; }

        public string DateGranted { get; set; }

        public string GranteeGroupId { get; set; }
    }
}
