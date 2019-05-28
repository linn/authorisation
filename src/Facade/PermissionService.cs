namespace Linn.Authorisation.Facade
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Facade.Exceptions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;

    public class PermissionService : FacadeService<Permission, int, PermissionResource, PermissionResource>, IPermissionService
    {
        private readonly IRepository<Privilege, int> privilegeRepository;

        private readonly IRepository<Group, int> groupRepository;

        private readonly IRepository<Permission, int> permissionRepository;

        public PermissionService(IRepository<Permission, int> repository, ITransactionManager transactionManager, IRepository<Privilege, int> privilegeRepository, IRepository<Group, int> groupRepository)
            : base(repository, transactionManager)
        {
            this.privilegeRepository = privilegeRepository;
            this.groupRepository = groupRepository;
            this.permissionRepository = repository;
        }

        public IResult<Permission> CreatePermission(PermissionResource resource)
        {
            if (resource.GranteeUri != null && resource.GroupName != null)
            {
                return new BadRequestResult<Permission>();
            }

            var result = this.Add(resource);
            if (result is BadRequestResult<Permission>)
            {
                return result;
            }
            var privilege = this.privilegeRepository.FilterBy(p => p.Name == resource.Privilege).FirstOrDefault();
            var group = this.groupRepository.FindAll().FirstOrDefault(g => g.Name == resource.GroupName);

            if (resource.GranteeUri != null && resource.GroupName == null)
            {
                return new CreatedResult<Permission>(new IndividualPermission(resource.GranteeUri, privilege, resource.GrantedByUri));
            }

            return new CreatedResult<Permission>(new GroupPermission(group, privilege, resource.GrantedByUri));
        }

        public IResult<Permission> RemovePermission(PermissionResource resource)
        {
            var permission = this.CreateFromResource(resource);
            if ((resource.GranteeUri == null && resource.GroupName == null) || (resource.GranteeUri != null && resource.GroupName != null))
            {
                return new BadRequestResult<Permission>();
            }

            this.permissionRepository.Remove(permission);
            
            return new SuccessResult<Permission>(permission);
        }

        protected override Permission CreateFromResource(PermissionResource resource)
        {
            var privilege = this.privilegeRepository.FilterBy(p => p.Name == resource.Privilege).FirstOrDefault();
            if (privilege == null)
            {
                throw new PrivilegeNotFoundException("Privilege Not Found");
            }

            if (resource.GranteeUri != null)
            {
                return new IndividualPermission(resource.GranteeUri, privilege, resource.GrantedByUri);
            }

            var group = this.groupRepository.FilterBy(g => g.Name == resource.GroupName).FirstOrDefault();
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
    }
}