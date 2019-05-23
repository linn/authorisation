namespace Linn.Authorisation.Domain.Repositories
{
    using Linn.Authorisation.Domain.Permissions;

    public interface IPrivilegeRepository
    {
        Privilege FindByName(string name);
    }
}
