namespace Linn.Authorisation.Service.Tests.Facade.Tests.AuthorisationServiceSpecs
{
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Domain.Services;
    using Linn.Authorisation.Facade;
    using Linn.Authorisation.Service.Tests.Facade.Tests;
    using Linn.Common.Persistence;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected MemberPrivilegesService Sut { get; private set; }

        protected IRepository<Permission, int> PermissionRepository{ get; private set; }

        protected IRepository<Group, int> GroupRepository { get; private set; }

        protected IPrivilegeService PrivilegeService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            TestDbContext.SetUp();
            this.PermissionRepository = new TestPermissionRepository();
            this.GroupRepository = new TestGroupRepository();
            this.PrivilegeService = new PrivilegeService(this.GroupRepository, this.PermissionRepository);
            this.Sut = new MemberPrivilegesService(this.PrivilegeService);
        }
    }
} 
