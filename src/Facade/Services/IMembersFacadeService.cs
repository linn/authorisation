namespace Linn.Authorisation.Facade.Services
{
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    public interface IMembersFacadeService
    {
        IResult<MemberResource> AddIndividualMember(MemberResource memberResource, string employeeUri);
    }
}