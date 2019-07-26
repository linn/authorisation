namespace Linn.Authorisation.Domain.Services
{
    using System.Collections.Generic;

    public interface IPrivilegeService
    {
        IEnumerable<Privilege> GetPrivileges(string who);
       
    }
}