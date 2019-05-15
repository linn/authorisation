namespace Linn.Authorisation.Facade
{
    using System.Collections.Generic;

    using Linn.Authorisation.Domain;
    using Linn.Common.Facade;

    public interface IAuthorisationService
    {
        IResult<IEnumerable<Permission>> GetPermissions(string who);
    }
}
