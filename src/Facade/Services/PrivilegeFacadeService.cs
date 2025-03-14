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
    using Linn.Common.Facade;
    using Linn.Common.Persistence;

    public class PrivilegeFacadeService : IPrivilegeFacadeService
    {
        private readonly IRepository<Privilege, int> privilegeRepository;

        private readonly IPrivilegeService privilegeService;

        private readonly IBuilder<Privilege> resourceBuilder;

        private readonly ITransactionManager transactionManager;

        public PrivilegeFacadeService(
            IRepository<Privilege, int> privilegeRepository,
            IPrivilegeService privilegeService,
            IBuilder<Privilege> resourceBuilder,
            ITransactionManager transactionManager)
        {
            this.privilegeRepository = privilegeRepository;
            this.privilegeService = privilegeService;
            this.resourceBuilder = resourceBuilder;
            this.transactionManager = transactionManager;
        }

        public IResult<IEnumerable<PrivilegeResource>> GetAllPrivlegesForUser(IEnumerable<string> privileges = null)
        {
            throw new NotImplementedException();
        }

        public IResult<IEnumerable<PrivilegeResource>> GetPrivlegeById(int privilegeId, IEnumerable<string> privileges = null)
        {
            throw new NotImplementedException();
        }

        IResult<PrivilegeResource> IPrivilegeFacadeService.CreatePrivlege(PrivilegeResource privilegeResource, string employeeUri, IEnumerable<string> privileges)
        {
            var privilege = new Privilege(privilegeResource.Name);

            return new SuccessResult<PrivilegeResource>(privilegeResource);
        }

        public void UpdatePrivilege(Privilege entity, PrivilegeResource updateResource, IEnumerable<string> privileges = null)
        {
            var privilegeList = this.privilegeRepository.FilterBy(g => g.Id != entity.Id);
            entity.Update(updateResource.Name, updateResource.Active);

            if (entity.CheckUnique(privilegeList))
            {
                return;
            }

            throw new DuplicatePrivilegeNameException("Privilege name already taken");
        }

        public IResult<PrivilegeResource> DeletePrivilege(int privilegeId)
        {
            throw new NotImplementedException();
        }
    }
}
