namespace Linn.Authorisation.Facade.Tests.PermissionServiceTests
{
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Domain.Repositories;
    using Linn.Authorisation.Domain.Services;
    using Linn.Common.Persistence;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected PermissionService Sut { get; private set; }

        protected IRepository<Permission, int> PermissionRepository { get; private set; }

        protected IPrivilegeRepository PrivilegeRepository { get; private set; }

        protected IGroupRepository GroupRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }
    
        [SetUp]
        public void SetUpContext()
        {
            this.PermissionRepository = Substitute.For<IRepository<Permission, int>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.PrivilegeRepository = Substitute.For<IPrivilegeRepository>();
            this.GroupRepository = Substitute.For<IGroupRepository>();
            this.Sut = new PermissionService(
                this.PermissionRepository,
                this.TransactionManager,
                this.PrivilegeRepository,
                this.GroupRepository);
        }
    }
}