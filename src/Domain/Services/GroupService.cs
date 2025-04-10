using System;
using Linn.Authorisation.Domain.Groups;

namespace Linn.Authorisation.Domain.Services
{
    using System.Collections.Generic;
    using Linn.Common.Persistence;

    public class GroupService : IGroupService
    {
        private readonly IRepository<Group, int> groupRepository;

        public GroupService(IRepository<Group, int> groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public IEnumerable<Group> GetAllGroupsForUser(IEnumerable<string> userPrivileges = null)
        {
            throw new NotImplementedException();
        }

        public Group GetGroupById(int privilegeId, IEnumerable<string> userPrivileges = null)
        {
            throw new NotImplementedException();
        }

    }
}