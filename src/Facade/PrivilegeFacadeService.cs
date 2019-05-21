namespace Linn.Authorisation.Facade
{
    using System;
    using System.Linq.Expressions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;

    public class PrivilegeFacadeService : FacadeService<Privilege, int, PrivilegeResource, PrivilegeResource>
    {
        public PrivilegeFacadeService(IRepository<Privilege, int> privilegeRepository, ITransactionManager transactionManager)
            : base(privilegeRepository, transactionManager)
        {
        }

        protected override Privilege CreateFromResource(PrivilegeResource resource)
        {
            return new Privilege(resource.Name);
        }

        protected override void UpdateFromResource(Privilege privilege, PrivilegeResource updateResource)
        {
            privilege.Update(updateResource.Name, updateResource.Active);
        }

        protected override Expression<Func<Privilege, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
