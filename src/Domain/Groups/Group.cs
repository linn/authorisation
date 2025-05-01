namespace Linn.Authorisation.Domain.Groups
{
    using Linn.Authorisation.Domain.Permissions;

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

        public IList<Member> Members { get; set; }

        public IList<GroupPermission> Permissions { get; set; }

        public void AddIndividualMember(string uri, string addedBy)
        {
            var existingIndividualMember =
                this.Members.SingleOrDefault(m => m is IndividualMember member && (member.MemberUri == uri));

            if (existingIndividualMember != null)
            {
                throw new MemberAlreadyInGroupException($"{uri} already exists in group {this.Name}");
            }

            this.Members.Add(new IndividualMember(uri, addedBy));
        }
        
        public void AddGroupMember(Group group, string addedBy)
        {
            if (group.Id == this.Id)
            {
                throw new GroupRecursionException("Cannot add a group to itself");
            }

            var existingGroupMember =
                (GroupMember)this.Members.SingleOrDefault(m => m is GroupMember member && (member.Id == group.Id));

            if (existingGroupMember != null)
            {
                throw new MemberAlreadyInGroupException(
                    $"Group {existingGroupMember.Group.Name} already exists in group {this.Name}");
            }

            this.Members.Add(new GroupMember(group, addedBy));
        }

        public bool CheckUnique(IEnumerable<Group> existingGroups)
        {
            return existingGroups.All(group => group.Name != this.Name);
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
