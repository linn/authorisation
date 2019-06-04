namespace Linn.Authorisation.Domain.Groups
{
    using System.Collections.Generic;
    using System.Linq;

    public class Group : Entity
    {
        public Group(string name, bool active)
        {
            this.Name = name;
            this.Active = active;
            this.Members = new List<Member>();
        }

        public Group()
        {
            this.Members = new List<Member>();
        }

        public string Name { get; set; }

        public bool Active { get; set; }

        public IList<Member> Members { get; private set; }

        public void AddIndividualMember(string uri, string addedBy)
        {
            this.Members.Add(new IndividualMember(uri, addedBy));
        }

        public void AddGroupMember(Group group, string addedBy)
        {
            this.Members.Add(new GroupMember(group, addedBy));
        }

        public IEnumerable<string> MemberUris()
        {
            return this.Members.SelectMany(s => s.MemberUris());
        }

        public bool IsMemberOf(string who)
        {
            return this.MemberUris().Contains(who);
        }
    }
}
