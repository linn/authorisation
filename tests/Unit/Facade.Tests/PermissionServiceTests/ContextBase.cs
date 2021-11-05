namespace Linn.Authorisation.Facade.Tests.PermissionServiceTests
{
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Domain.Services;
    using Linn.Common.Persistence;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected PermissionFacadeService Sut { get; private set; }

        protected IRepository<Permission, int> PermissionRepository { get; private set; }

        protected IRepository<Privilege, int> PrivilegeRepository { get; private set; }

        protected IRepository<Group, int> GroupRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        protected IPermissionService PermissionService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.PermissionRepository = Substitute.For<IRepository<Permission, int>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.PrivilegeRepository = Substitute.For<IRepository<Privilege, int>>();
            this.GroupRepository = Substitute.For<IRepository<Group, int>>();
            this.PermissionService = Substitute.For<IPermissionService>();
            this.Sut = new PermissionFacadeService(
                this.PermissionRepository,
                this.TransactionManager,
                this.PrivilegeRepository,
                this.GroupRepository,
                this.PermissionService);
        }
    }
}