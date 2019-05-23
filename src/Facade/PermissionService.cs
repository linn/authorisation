namespace Linn.Authorisation.Facade
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Domain.Repositories;
    using Linn.Authorisation.Resources;
    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;

    public class PermissionService : FacadeService<Permission, int, PermissionCreateResource, PermissionResource>, IPermissionService
    {
        private readonly IPrivilegeRepository privilegeRepository;

        private readonly IGroupRepository groupRepository;

        public PermissionService(IRepository<Permission, int> repository, ITransactionManager transactionManager, IPrivilegeRepository privilegeRepository, IGroupRepository groupRepository )
            : base(repository, transactionManager)
        {
            this.privilegeRepository = privilegeRepository;
            this.groupRepository = groupRepository;
        }

        protected override Permission CreateFromResource(PermissionCreateResource resource)
        {
            var privilege = this.privilegeRepository.FindByName(resource.Privilege);

            if (resource.GranteeUri != null)
            {
                return new IndividualPermission(resource.GranteeUri, privilege, resource.GrantedByUri);
            }

            var group = this.groupRepository.GetGroups().FirstOrDefault(g => g.Name == resource.GroupName);
            return new GroupPermission(group, privilege, resource.GrantedByUri);
        }

        protected override void UpdateFromResource(Permission entity, PermissionResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<Permission, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public IResult<Permission> CreatePermission(PermissionCreateResource resource)
        {
            if ((resource.GranteeUri == null && resource.GroupName == null) || (resource.GranteeUri != null && resource.GroupName != null))
            {
                return new BadRequestResult<Permission>();
            }

            var result = this.Add(resource);
            if (result is BadRequestResult<Permission>)
            {
                return result;
            }
            var privilege = this.privilegeRepository.FindByName(resource.Privilege);
            var group = this.groupRepository.GetGroups().FirstOrDefault(g => g.Name == resource.GroupName);

            if (resource.GranteeUri != null && resource.GroupName == null)
            {
                return new CreatedResult<Permission>(new IndividualPermission(resource.GranteeUri, privilege, resource.GrantedByUri));
            }
            return new CreatedResult<Permission>(new GroupPermission(group, privilege, resource.GrantedByUri));

        }
    }
}