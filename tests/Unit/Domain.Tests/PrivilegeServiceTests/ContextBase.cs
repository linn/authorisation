namespace Linn.Authorisation.Domain.Tests.PrivilegeServiceTests
{
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Domain.Services;
    using Linn.Common.Persistence;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected IRepository<Group, int> GroupRepository { get; private set; }

        protected IRepository<Permission, int> PermissionRepository { get; private set; }

        protected IPrivilegeService Sut { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.PermissionRepository = Substitute.For<IRepository<Permission, int>>();
            this.GroupRepository = Substitute.For<IRepository<Group, int>>();

            this.Sut = new PrivilegeService(this.GroupRepository, this.PermissionRepository);
        }
    }
}
