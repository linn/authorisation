namespace Linn.Authorisation.Domain.Services
{
    using System.Collections.Generic;

    public interface IPrivilegeService
    {
        IEnumerable<Privilege> GetPrivileges(string who);
        IEnumerable<Privilege> GetImmediatePrivilegesForGroup(int groupId);

        IEnumerable<Privilege> GetAllPrivilegesForGroup(int groupId);
    }
}