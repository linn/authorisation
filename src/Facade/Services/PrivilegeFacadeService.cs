namespace Linn.Authorisation.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Exceptions;
    using Linn.Authorisation.Domain.Services;
    using Linn.Authorisation.Resources;
    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;

    public class PrivilegeFacadeService : FacadeResourceService<Privilege, int, PrivilegeResource, PrivilegeResource>
    {
        private readonly IPrivilegeService privilegeService;

        private readonly IAuthorisationService authService;

        private readonly IBuilder<Privilege> resourceBuilder;

        private readonly IRepository<Privilege, int> repository;

        public PrivilegeFacadeService(
            IRepository<Privilege, int> repository,
            IBuilder<Privilege> resourceBuilder,
            ITransactionManager transactionManager,
            IPrivilegeService privilegeService,
            IAuthorisationService authService)
            : base(repository, transactionManager, resourceBuilder)
        {
            this.privilegeService = privilegeService;
            this.resourceBuilder = resourceBuilder;
            this.repository = repository;
            this.authService = authService;
        }

        public override IResult<IEnumerable<PrivilegeResource>> GetAll(IEnumerable<string> userPrivileges = null)
        {
            var privileges = this.privilegeService.GetAllPrivilegesForUser(userPrivileges);

            var resources = privileges.Select(x => (PrivilegeResource)this.resourceBuilder.Build(x, userPrivileges)).ToList();

            return new SuccessResult<IEnumerable<PrivilegeResource>>(resources);
        }

        public override IResult<PrivilegeResource> GetById(int id, IEnumerable<string> userPrivileges = null)
        {
            var entity = this.privilegeService.GetPrivilegeById(id, userPrivileges);

            var resource = (PrivilegeResource)this.resourceBuilder.Build(entity, userPrivileges);

            return new SuccessResult<PrivilegeResource>(resource);
        }

        protected override Privilege CreateFromResource(PrivilegeResource resource, IEnumerable<string> userPrivileges = null)
        {
            if (!this.authService.HasPermissionFor(AuthorisedAction.AuthorisationAuthManager, userPrivileges))
            {
                throw new UnauthorisedActionException("You do not have permission to create this privilege");
            }

            var privilege = new Privilege
            {
                Active = true,
                Name = resource.Name,
            };

            this.repository.Add(privilege);

            return privilege;
        }

        protected override void UpdateFromResource(
            Privilege entity,
            PrivilegeResource updateResource,
            IEnumerable<string> userPrivileges = null)
        { 
            if (!this.authService.HasPermissionFor(AuthorisedAction.AuthorisationAuthManager, userPrivileges)) 
            { 
                throw new UnauthorisedActionException("You do not have permission to create this privilege");
            }

            var privilegeList = this.repository.FilterBy(g => g.Id != entity.Id);

            entity.Update(updateResource.Name, updateResource.Active);

            if (entity.CheckUnique(privilegeList))
            {
                return;
            }

            throw new DuplicatePrivilegeNameException("Privilege name already taken");
        }

        protected override Expression<Func<Privilege, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }

        protected override void SaveToLogTable(
            string actionType,
            int userNumber,
            Privilege entity,
            PrivilegeResource resource,
            PrivilegeResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override void DeleteOrObsoleteResource(Privilege entity, IEnumerable<string> privileges = null)
        {
            throw new NotImplementedException();
        }
    }
}
