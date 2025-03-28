namespace Linn.Authorisation.Domain.Services
{
    using System.Collections.Generic;

    using Linn.Authorisation.Domain.Permissions;

    public interface IPrivilegeService
    {
        IEnumerable<Privilege> GetAllPrivilegesForUser(IEnumerable<string> privileges = null);

        Privilege GetPrivilegeById(int privilegeId, IEnumerable<string> privileges = null);
    }
}
