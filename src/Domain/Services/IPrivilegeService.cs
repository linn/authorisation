namespace Linn.Authorisation.Domain.Services
{
    using System.Collections.Generic;

    public interface IPrivilegeService
    {
        IEnumerable<Privilege> GetAllPrivilegesForUser(IEnumerable<string> privileges = null);

        Privilege GetPrivilegeById(int privilegeId, IEnumerable<string> privileges = null);
    }
}
