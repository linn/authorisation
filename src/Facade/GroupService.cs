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
            // not sure how you can write an expression to find all groups who is a member of if
            // you allow the possibility of groups on groups so bringing them all back is easier.
            var groups = groupRepository.FindAll().ToList();
            return groups.Where(g => g.IsMemberOf(who));
        }
    }
}