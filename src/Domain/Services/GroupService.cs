namespace Linn.Authorisation.Domain.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Common.Persistence;

    public class GroupService : IGroupService
    {
        private readonly IRepository<Group, int> groupRepository;

        public GroupService(IRepository<Group, int> groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public IEnumerable<Group> GetGroupMemberships(string who)
        {
            return this.groupRepository.FindAll().Where(g => g.IsMemberOf(who));
        }
    }
}
