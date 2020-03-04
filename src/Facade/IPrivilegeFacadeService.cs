namespace Linn.Authorisation.Facade
{
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    public interface IPrivilegeFacadeService : IFacadeService<Privilege, int, PrivilegeResource, PrivilegeResource>
    {
        IResult<string> Remove(int id);
    }
}
