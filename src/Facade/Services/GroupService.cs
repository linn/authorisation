namespace Linn.Authorisation.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;

    public class GroupService : FacadeResourceService<Group, int, GroupResource, GroupResource> 

    {
        public GroupService(IRepository<Group, int> repository, ITransactionManager transactionManager, IBuilder<Group> resourceBuilder)
            : base(repository, transactionManager, resourceBuilder)
        {
        }

        protected override Group CreateFromResource(GroupResource resource, IEnumerable<string> privileges = null)
        {
            var active = true;
            var group = new Group(resource.Name,active);
            return group;
        }

        protected override void UpdateFromResource(Group entity, GroupResource updateResource, IEnumerable<string> privileges = null)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<Group, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }

        protected override void SaveToLogTable(
            string actionType,
            int userNumber,
            Group entity,
            GroupResource resource,
            GroupResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override void DeleteOrObsoleteResource(Group entity, IEnumerable<string> privileges = null)
        {
            throw new NotImplementedException();
        }
    }
}