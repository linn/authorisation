namespace Linn.Authorisation.Facade
{
    using System.Collections.Generic;
    using Domain;
    using Domain.Groups;

    public interface IGroupService
    {
        IEnumerable<Group> GetGroups(string who);
    }
}