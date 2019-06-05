namespace Linn.Authorisation.Domain.Groups
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;

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
            var existingIndividualMember =
                this.Members.SingleOrDefault(m => m is IndividualMember && (((IndividualMember) m).MemberUri == uri));

            if (existingIndividualMember != null)
            {
                throw new MemberAlreadyInGroupException($"{uri} already exists in group {this.Name}");
            }

            this.Members.Add(new IndividualMember(uri, addedBy));
        }

        public void AddGroupMember(Group group, string addedBy)
        {
            if (this.Id == group?.Id)
            {
                throw new MemberAlreadyInGroupException($"cannot make {group.Name} a member of itself");
            }

            var existingGroupMember =
                this.Members.SingleOrDefault(m => m is GroupMember && (((GroupMember)m).Group.Id == group.Id));

            if (existingGroupMember != null)
            {
                throw new MemberAlreadyInGroupException($"group {group.Name} already exists in group {this.Name}");
            }

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
