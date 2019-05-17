namespace Linn.Authorisation.Facade
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Persistence;
    using Domain;
    using Domain.Groups;

    public class GroupService : IGroupService
    {
        private readonly IRepository<Group, int> groupRepository;

        public GroupService(IRepository<Group, int> groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public IEnumerable<Group> GetGroups(string who)
        {
            var groups = groupRepository.FindAll().ToList();
            return groups.Where(g => g.IsMemberOf(who));
        }
    }
}