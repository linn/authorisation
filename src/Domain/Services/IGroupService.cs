namespace Linn.Authorisation.Domain.Services
{
    using Linn.Authorisation.Domain.Groups;
    using System.Collections.Generic;

    public interface IGroupService
    {
        IEnumerable<Group> GetAllGroupsForUser(IEnumerable<string> privileges = null);

        Group GetGroupById(int groupId, IEnumerable<string> privileges = null);
    }
}