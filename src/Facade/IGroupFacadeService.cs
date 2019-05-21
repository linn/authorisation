namespace Linn.Authorisation.Facade
{
    using System.Collections.Generic;
    using Domain.Groups;
    using Linn.Common.Facade;

    public interface IGroupFacadeService
    {
        IResult<IEnumerable<Group>> GetGroupMemberships(string who);
    }
}