namespace Linn.Authorisation.Service.Tests.Facade.Tests.AuthorisationServiceSpecs
{
    using Domain.Groups;
    using Domain.Repositories;
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Services;
    using Linn.Authorisation.Facade;
    using Linn.Authorisation.Service.Tests.Facade.Tests;
    using Linn.Common.Persistence;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected AuthorisationService Sut { get; private set; }

        protected IPermissionRepository PermissionRepository{ get; private set; }

        protected IRepository<Group, int> GroupRepository { get; private set; }

        protected GroupService GroupService { get; private set; }

        protected PrivilegeService PrivilegeService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            TestDbContext.SetUp();
            this.PermissionRepository = new TestPermissionRepository();
            this.GroupRepository = new TestGroupRepository();
            this.GroupService = new GroupService(this.GroupRepository);
            this.PrivilegeService = new PrivilegeService(this.GroupRepository, this.PermissionRepository);
            this.Sut = new AuthorisationService(this.PrivilegeService);
        }
    }
} 
