namespace Linn.Authorisation.Domain.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Authorisation.Domain.Exceptions;
    using Linn.Authorisation.Domain.Groups;

    public class GroupService : IGroupService
    {
        private readonly IRepository<Group, int> groupRepository;

        public GroupService(IRepository<Group, int> groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public IEnumerable<Group> GetAllGroupsForUser(IEnumerable<string> userPrivileges = null)
        {
            if (userPrivileges.Contains("authorisation.auth-manager"))
            {
                return this.groupRepository.FindAll();
            }

            if (userPrivileges == null || !userPrivileges.Any())
            {
                return new List<Group>();
            }

            var groupSuperUsers = userPrivileges
                .Where(p => p.ToLower().Contains("auth-manager"))
                .Select(p => p.Split('.')[0])
                .Distinct()
                .ToList();

            if (!groupSuperUsers.Any())
            {
                return new List<Group>();
            }

            var resultList = this.groupRepository
                .FindAll()
                .AsEnumerable()
                .Where(p => groupSuperUsers.Contains(p.Name.Split('.')[0]))
                .ToList();

            return resultList;
        }

        public Group GetGroupById(int groupId, IEnumerable<string> userPrivileges = null)
        {
            if (userPrivileges.Contains("authorisation.auth-manager"))
            {
                return this.groupRepository.FindById(groupId);
            }

            if (userPrivileges == null || !userPrivileges.Any())
            {
                throw new UnauthorisedActionException("You do not have any permissions");
            }

            var groupSuperUsers = userPrivileges
                .Where(p => p.ToLower().Contains("auth-manager"))
                .Select(p => p.Split('.')[0])
                .Distinct()
                .ToList();

            var result = this.groupRepository
                .FindById(groupId);

            if (groupSuperUsers.Contains(result.Name.Split(".")[0]))
            {
                return result;
            }

            throw new UnauthorisedActionException("You do not have the permission to access this group");
        }

    }
}