namespace Linn.Authorisation.Resources
{
    public class MemberResource
    {
        public int Id { get; set;  }

        public string MemberUri { get; set; }

        public int? GroupId { get; set; }

        public string DateAdded { get; set; }

        public string AddedByUri { get; set; }
    }
}

