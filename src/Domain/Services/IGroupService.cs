namespace Linn.Authorisation.Domain.Services
{
    using System.Collections.Generic;

    using Linn.Authorisation.Domain.Groups;

    public interface IGroupService
    {
        IEnumerable<Group> GetGroupMemberships(string who);
    }
}
