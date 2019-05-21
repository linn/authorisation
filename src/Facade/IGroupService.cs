namespace Linn.Authorisation.Facade
{
    using System.Collections.Generic;
    using Domain;
    using Domain.Groups;
    using Linn.Common.Facade;

    public interface IGroupService
    {
        IResult<IEnumerable<Group>> GetGroups(string who);
    }
}