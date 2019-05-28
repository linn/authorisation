namespace Linn.Authorisation.Service.Tests.Facade.Tests.PermissionServiceSpecs
{
    using System;
    using System.Linq.Expressions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Facade;
    using Linn.Common.Persistence;

    using NSubstitute;
    using NSubstitute.ReturnsExtensions;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected PermissionService Sut { get; private set; }

        protected IRepository<Permission, int> PermissionRepository { get; private set; }

        protected IRepository<Group, int> GroupRepository { get; private set; }

        protected IRepository<Privilege, int> PrivilegeRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            TestDbContext.SetUp();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.PermissionRepository = Substitute.For<IRepository<Permission, int>>();
            this.PrivilegeRepository = Substitute.For<IRepository<Privilege, int>>();
            this.GroupRepository = Substitute.For<IRepository<Group, int>>();

            this.Sut = new PermissionService(this.PermissionRepository, this.TransactionManager, this.PrivilegeRepository, this.GroupRepository);
        }
    }
}
