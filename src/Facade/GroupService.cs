namespace Linn.Authorisation.Facade
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Persistence;
    using Domain.Groups;

    using Linn.Common.Facade;

    public class GroupService : IGroupService
    {
        private readonly IRepository<Group, int> groupRepository;

        public GroupService(IRepository<Group, int> groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public IResult<IEnumerable<Group>> GetGroups(string who)
        {
            var groups = this.groupRepository.FindAll().Where(g => g.IsMemberOf(who));
            return new SuccessResult<IEnumerable<Group>>(groups);
        }

    }
}