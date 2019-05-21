namespace Linn.Authorisation.Domain.Repositories
{
    using System.Collections.Generic;

    using Linn.Authorisation.Domain.Groups;

    public interface IGroupRepository
    {
        IEnumerable<Group> GetGroups();
    }
}
