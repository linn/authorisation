namespace Linn.Authorisation.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Linn.Authorisation.Domain.Exceptions;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Domain.Services;
    using Linn.Authorisation.Persistence;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;

    public class GroupFacadeService : FacadeResourceService<Group, int, GroupResource, GroupResource>
    {
        private readonly IRepository<Group, int> groupRepository;

        private readonly ITransactionManager transactionManager;

        public GroupFacadeService(
            IRepository<Group, int> repository,
            ITransactionManager transactionManager,
            IBuilder<Group> resourceBuilder,
            IRepository<Group, int> groupRepository)
            : base(repository, transactionManager, resourceBuilder)
        {
            this.groupRepository = groupRepository;
        }

        protected override Group CreateFromResource(GroupResource resource, IEnumerable<string> privileges = null)
        {
            var active = true;
            var group = new Group(resource.Name, resource.Active);

            var groups = this.groupRepository.FilterBy(g => g is Group);

            if (group.CheckUnique(groups))
            {
                this.groupRepository.Add(group);
                this.transactionManager.Commit();

                var result = new Group { Name = resource.Name, Active = resource.Active, };
                return(result);
            }
            throw new DuplicateGroupNameException("Group name already taken");
            
        }

        protected override void UpdateFromResource(
            Group entity,
            GroupResource updateResource,
            IEnumerable<string> privileges = null)
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