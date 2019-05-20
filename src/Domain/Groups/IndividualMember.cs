namespace Linn.Authorisation.Domain.Groups
{
    using System;
    using System.Collections.Generic;

    public class IndividualMember : Member
    {
        public IndividualMember(string uri, string addedByUri)
        {
            this.MemberUri = uri;
            this.AddedByUri = addedByUri;
            this.DateAdded = DateTime.UtcNow;
        }

        public string MemberUri { get; set; }

        public override IEnumerable<string> MemberUris()
        {
            return new List<string>() {this.MemberUri};
        }
    }
}