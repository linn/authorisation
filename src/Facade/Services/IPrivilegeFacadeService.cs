namespace Linn.Authorisation.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    public interface IPrivilegeFacadeService
    {
        IResult<IEnumerable<PrivilegeResource>> GetAllPrivilegesForUser(IEnumerable<string> privileges = null);

        IResult<PrivilegeResource> GetPrivilegeById(int privilegeId, IEnumerable<string> privileges = null);

        IResult<PrivilegeResource> CreatePrivilege(PrivilegeResource privilegeResource, IEnumerable<string> privileges = null);

        IResult<PrivilegeResource> UpdatePrivilege(int id, PrivilegeResource updateResource, IEnumerable<string> userPrivileges = null);

        IResult<PrivilegeResource> DeletePrivilege(int privilegeId);
    }
}
