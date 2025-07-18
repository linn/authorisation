﻿namespace Linn.Authorisation.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Exceptions;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Services;
    using Linn.Authorisation.Resources;
    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;

    public class GroupFacadeService : FacadeResourceService<Group, int, GroupResource, GroupResource>
    {
        private readonly IRepository<Group, int> groupRepository;

        private readonly IAuthorisationService authService;

        private readonly IGroupService groupService;

        private readonly IBuilder<Group> resourceBuilder;

        public GroupFacadeService(
            IRepository<Group, int> repository,
            ITransactionManager transactionManager,
            IBuilder<Group> resourceBuilder,
            IRepository<Group, int> groupRepository,
            IAuthorisationService authService,
            IGroupService groupService)
            : base(repository, transactionManager, resourceBuilder)
        {
            this.groupRepository = groupRepository;
            this.authService = authService;
            this.groupService = groupService;
            this.resourceBuilder = resourceBuilder;
        }

        public override IResult<IEnumerable<GroupResource>> GetAll(IEnumerable<string> userPrivileges = null)
        {
            var groups = this.groupService.GetAllGroupsForUser(userPrivileges);

            var resources = groups.Select(x => (GroupResource)this.resourceBuilder.Build(x, userPrivileges)).ToList();

            return new SuccessResult<IEnumerable<GroupResource>>(resources);
        }

        public override IResult<GroupResource> GetById(int id, IEnumerable<string> userPrivileges = null)
        {
            var entity = this.groupService.GetGroupById(id, userPrivileges);

            var resource = (GroupResource)this.resourceBuilder.Build(entity, userPrivileges);

            return new SuccessResult<GroupResource>(resource);
        }

        protected override Group CreateFromResource(GroupResource resource, IEnumerable<string> userPrivileges = null)
        {
            if (!this.authService.HasPermissionFor(AuthorisedAction.AuthorisationAuthManager, userPrivileges))
            {
                throw new UnauthorisedActionException("You do not have permission to create this group");
            }

            var group = new Group(resource.Name, true);

            var groups = this.groupRepository.FindAll();

            if (group.CheckUnique(groups))
            {
                return (group);
            }

            throw new DuplicateGroupNameException("Group name already taken");
        }

        protected override void UpdateFromResource(
            Group entity,
            GroupResource updateResource,
            IEnumerable<string> userPrivileges = null)
        {
            if (!this.authService.HasPermissionFor(AuthorisedAction.AuthorisationAuthManager, userPrivileges))
            {
                throw new UnauthorisedActionException("You do not have permission to update this group");
            }

            var groups = this.groupRepository.FilterBy(g => g.Id != entity.Id);
            
            entity.Update(updateResource.Name, updateResource.Active);

            if (entity.CheckUnique(groups))
            {
                return;
            }
            
            throw new DuplicateGroupNameException("Group name already taken");
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