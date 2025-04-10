using System.Collections;
using System.Linq;
using Linn.Common.Proxy.LinnApps;

namespace Linn.Authorisation.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq.Expressions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Exceptions;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Domain.Services;
    using Linn.Authorisation.Resources;
    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;

    public class PrivilegeFacadeService : IPrivilegeFacadeService
    {
        private readonly IRepository<Privilege, int> privilegeRepository;

        private readonly IPrivilegeService privilegeService;

        private readonly IBuilder<Privilege> resourceBuilder;

        private readonly ITransactionManager transactionManager;

        private readonly IAuthorisationService authService;

        public PrivilegeFacadeService(
            IRepository<Privilege, int> privilegeRepository,
            IPrivilegeService privilegeService,
            IBuilder<Privilege> resourceBuilder,
            ITransactionManager transactionManager,
            IAuthorisationService authService)
        {
            this.privilegeRepository = privilegeRepository;
            this.privilegeService = privilegeService;
            this.resourceBuilder = resourceBuilder;
            this.transactionManager = transactionManager;
            this.authService = authService;
        }

        public IResult<IEnumerable<PrivilegeResource>> GetAllPrivilegesForUser(IEnumerable<string> userPrivileges = null)
        {
            var privileges = this.privilegeService.GetAllPrivilegesForUser(userPrivileges);

            var resources = privileges.Select(x => (PrivilegeResource)this.resourceBuilder.Build(x, userPrivileges));

            return new SuccessResult<IEnumerable<PrivilegeResource>>(resources);
        }

        public IResult<PrivilegeResource> GetPrivilegeById(int privilegeId, IEnumerable<string> userPrivileges = null)
        {
            var privilege = this.privilegeService.GetPrivilegeById(privilegeId, userPrivileges);

            var resource = (PrivilegeResource)this.resourceBuilder.Build(privilege, userPrivileges);

            return new SuccessResult<PrivilegeResource>(resource);
        }

        public IResult<PrivilegeResource> CreatePrivilege(PrivilegeResource privilegeResource, IEnumerable<string> userPrivileges)
        {
            if (!userPrivileges.Contains($"{privilegeResource.Name.Split('.')[0]}.super-user") && !this.authService.HasPermissionFor(AuthorisedAction.AuthorisationSuperUser, userPrivileges))
            {
                throw new LackingPermissionException("You do not have permission to create this privilege");
            }

            var privilege = new Privilege
            {
                Active = true,
                Name = privilegeResource.Name,
            };

            this.privilegeRepository.Add(privilege);
            this.transactionManager.Commit();

            return new CreatedResult<PrivilegeResource>(privilegeResource);
        }

        public IResult<PrivilegeResource> UpdatePrivilege(int id, PrivilegeResource updateResource, IEnumerable<string> userPrivileges = null)
        {
            var entity = this.privilegeService.GetPrivilegeById(id, userPrivileges);

            var privilegeList = this.privilegeRepository.FilterBy(g => g.Id != entity.Id);

            entity.Update(updateResource.Name, updateResource.Active);

            if (entity.CheckUnique(privilegeList))
            {
                this.transactionManager.Commit();
                return new SuccessResult<PrivilegeResource>(updateResource);
            }

            throw new DuplicatePrivilegeNameException("Privilege name already taken");
        }

        public IResult<PrivilegeResource> DeletePrivilege(int privilegeId)
        {
            throw new NotImplementedException();
        }
    }
}
