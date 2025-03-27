using Linn.Authorisation.Domain;

namespace Linn.Authorisation.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    public interface IPrivilegeFacadeService
    {
        IResult<IEnumerable<PrivilegeResource>> GetAllPrivlegesForUser( IEnumerable<string> privileges = null);

        IResult<PrivilegeResource> GetPrivlegeById(int privilegeId, IEnumerable<string> privileges = null);

        IResult<PrivilegeResource> CreatePrivlege(PrivilegeResource privilegeResource, string employeeUri, IEnumerable<string> privileges = null);

        void UpdatePrivilege(Privilege entity, PrivilegeResource updateResource, IEnumerable<string> privileges = null);

        IResult<PrivilegeResource> DeletePrivilege(int privilegeId);
    }
}
