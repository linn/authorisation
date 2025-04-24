namespace Linn.Authorisation.Domain.Tests.PermissionServiceTests
{
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Domain.Services;
    using Linn.Common.Persistence;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IRepository<Group, int> GroupRepository { get; private set; }

        protected IRepository<Permission, int> PermissionRepository { get; private set; }

        protected IPermissionService Sut { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.GroupRepository = Substitute.For<IRepository<Group, int>>();
            this.PermissionRepository = Substitute.For<IRepository<Permission, int>>();
            this.Sut = new PermissionService(this.GroupRepository, this.PermissionRepository);
        }
    }
}
