namespace Linn.Authorisation.Domain.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Repositories;

    public class GroupService : IGroupService
    {
        private readonly IGroupRepository groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public IEnumerable<Group> GetGroupMemberships(string who)
        {
            return this.groupRepository.GetGroups().Where(g => g.IsMemberOf(who));
        }
    }
}
