namespace Linn.Authorisation.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Facade;
    using Common.Resources;
    using Domain;
    using Domain.Groups;
    using Resources;

    public class GroupResourceBuilder : IResourceBuilder<Group>
    {
        public GroupResource Build(Group group)
        {
            return new GroupResource
            {
                Name = group.Name,
                Active = group.Active,
                Members = group.Members.Select(m => this.Build(m, group)),
                Links = this.BuildLinks(group).ToArray()
            };
        }

        private GroupMemberResource Build(Member member, Group group)
        {
            if (member is IndividualMember individualMember)
            {
                return new GroupMemberResource
                {
                    MemberUri = individualMember.MemberUri,
                    AddedByUri = member.AddedByUri,
                    Links = this.BuildLinks(member, group).ToArray()
                };
            }
            if (member is GroupMember groupMember)
            {
                return new GroupMemberResource
                {
                    GroupName =  groupMember.Group.Name,
                    AddedByUri = member.AddedByUri,
                    Links = this.BuildLinks(member, group).ToArray()
                };
            }
            return null;
        }

        object IResourceBuilder<Group>.Build(Group g) => this.Build(g);

        public string GetLocation(Group group)
        {
            return $"/authorisation/groups/{group.Id}";
        }

        public IEnumerable<LinkResource> BuildLinks(Group group)
        {
            yield return new LinkResource
            {
                Rel = "self",
                Href = $"/authorisation/groups/{group.Id}"
            };
        }

        public IEnumerable<LinkResource> BuildLinks(Member member, Group group)
        {
            yield return new LinkResource
            {
                Rel = "self",
                Href = $"/authorisation/groups/{group.Id}/members/{member.Id}"
            };
        }
    }   
}