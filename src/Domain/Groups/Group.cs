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

        public Group(string name, bool active, int id)
        {
            this.Name = name;
            this.Active = active;
            this.Id = id;
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

        public bool CheckUnique(IEnumerable<Group> existingGroups)
        {
            foreach (var group in existingGroups)
            {
                if (group.Name == this.Name)
                {
                    return false;
                }
            }
            return true;
        }

        public void RemoveMember(Member member)
        {
            this.Members.Remove(member);
        }

        public IEnumerable<string> MemberUris()
        {
            return this.Members.SelectMany(s => s.MemberUris());
        }

        public bool IsMemberOf(string who)
        {
            return this.MemberUris().Contains(who);
        }

        public void Update(string name, bool active)
        {
            this.Name = name;
            this.Active = active;
        }
    }
}
