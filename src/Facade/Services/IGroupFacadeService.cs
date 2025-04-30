namespace Linn.Authorisation.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    public interface IGroupFacadeService : IFacadeResourceService<Group, int, GroupResource, GroupResource>
    {
        void DeleteMember(int groupId, string employeeUri, int memberId, IEnumerable<string> userPrivileges = null);
    }
}
