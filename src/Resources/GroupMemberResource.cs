namespace Linn.Authorisation.Resources
{
    using Common.Resources;

    public class GroupMemberResource : HypermediaResource
    {
        public string MemberUri { get; set; }

        public string GroupName { get; set; }

        public string AddedByUri { get; set; }

    }
}