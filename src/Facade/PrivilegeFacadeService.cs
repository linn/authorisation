namespace Linn.Authorisation.Facade
{
    using System;
    using System.Linq.Expressions;
    using System.Net;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Exceptions;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;

    public class PrivilegeFacadeService : FacadeService<Privilege, int, PrivilegeResource, PrivilegeResource>, IPrivilegeFacadeService
    {
        private readonly IRepository<Privilege, int> privilegeRepository;

        private readonly IRepository<Permission, int> permissionRepository;

        private readonly ITransactionManager transactionManager;

        public PrivilegeFacadeService(IRepository<Privilege, int> privilegeRepository, ITransactionManager transactionManager, IRepository<Permission, int> permissionRepository)
            : base(privilegeRepository, transactionManager)
        {
            this.privilegeRepository = privilegeRepository;
            this.transactionManager = transactionManager;
            this.permissionRepository = permissionRepository;
        }

        public IResult<Privilege> Remove(int privilegeId)
        {
            var permissionsForPriv = this.permissionRepository.FilterBy(x => x.Privilege.Id == privilegeId);
            var privilegeToRemove = this.privilegeRepository.FindBy(x => x.Id == privilegeId);

            try
            {
                foreach (var p in permissionsForPriv)
                {
                    this.permissionRepository.Remove(p);
                }

                this.privilegeRepository.Remove(privilegeToRemove);
            }
            catch (DomainException exception)
            {
                return new BadRequestResult<Privilege>($"Error removing privilege {privilegeId} or associated permissions - {exception.Message}");
            }

            this.transactionManager.Commit();

            return new SuccessResult<Privilege>(privilegeToRemove); 
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
