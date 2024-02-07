namespace Linn.Authorisation.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;

    public class PrivilegeFacadeService : FacadeResourceService<Privilege, int, PrivilegeResource, PrivilegeResource>
    {
        public PrivilegeFacadeService(IRepository<Privilege, int> repository, ITransactionManager transactionManager, IBuilder<Privilege> resourceBuilder) 
            : base(repository, transactionManager, resourceBuilder)
        {
        }
        
        protected override Privilege CreateFromResource(PrivilegeResource resource, IEnumerable<string> privileges = null)
        {
            var privilege = new Privilege(resource.Name);
            return privilege;
        }
        
        protected override void UpdateFromResource(Privilege entity, PrivilegeResource updateResource, IEnumerable<string> privileges = null)
        {
            entity.Update(updateResource.Name, updateResource.Active);
        }
        
        protected override Expression<Func<Privilege, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
        
        protected override void SaveToLogTable(string actionType, int userNumber, Privilege entity, PrivilegeResource resource, PrivilegeResource updateResource)
        {
            throw new NotImplementedException();
        }
        
        protected override void DeleteOrObsoleteResource(Privilege entity, IEnumerable<string> privileges = null)
        {
            throw new NotImplementedException();
        }
    }
}
