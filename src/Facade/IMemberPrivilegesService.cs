namespace Linn.Authorisation.Facade
{
    using System.Collections.Generic;
    using Linn.Authorisation.Domain;
    using Linn.Common.Facade;

    public interface IMemberPrivilegesService
    {
        IResult<IEnumerable<Privilege>> GetPrivilegesForMember(string who);
    }
}
