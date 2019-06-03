namespace Linn.Authorisation.Facade
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Domain.Groups;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using PagedList.Core;

    public class GroupFacadeService : FacadeService<Group, int, GroupResource, GroupResource>
    {
        public GroupFacadeService(IRepository<Group, int> groupRepository, ITransactionManager transactionManager) 
            : base(groupRepository, transactionManager)
        {
        }

        protected override Group CreateFromResource(GroupResource resource)
        {
            return new Group(resource.Name, resource.Active);
        }

        protected override void UpdateFromResource(Group group, GroupResource updateResource)
        {
            group.Name = updateResource.Name;
            group.Active = updateResource.Active;
        }

        protected override Expression<Func<Group, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
