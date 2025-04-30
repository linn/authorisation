namespace Linn.Authorisation.Facade.Services
{
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using System.Collections.Generic;

    public interface IMembersFacadeService
    {
        IResult<MemberResource> AddIndividualMember(MemberResource memberResource, string employeeUri, IEnumerable<string> privileges = null);

        IResult<MemberResource> DeleteMember(int memberId);
    }
}