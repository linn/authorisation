namespace Linn.Authorisation.Facade.Tests.AuthorisationServiceTests
{
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Domain.Services;
    using Linn.Common.Persistence;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected MemberPrivilegesService Sut { get; private set; }

        protected IRepository<Permission, int> PermissionRepository { get; private set; }

        protected IRepository<Group, int> GroupRepository { get; private set; }

        protected IPrivilegeService PrivilegeService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.PermissionRepository = Substitute.For<IRepository<Permission, int>>();
            this.GroupRepository = Substitute.For<IRepository<Group, int>>();
            this.PrivilegeService = new PrivilegeService(this.GroupRepository, this.PermissionRepository);

            this.Sut = new MemberPrivilegesService(this.PrivilegeService);
        }
    }
}
