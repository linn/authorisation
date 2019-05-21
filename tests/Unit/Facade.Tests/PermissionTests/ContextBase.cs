namespace Linn.Authorisation.Facade.Tests.PermissionTests
{
    using Domain.Repositories;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Services;
    using Linn.Common.Persistence;

    using NSubstitute;
    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected AuthorisationService Sut { get; private set; }

        protected IPermissionRepository PermissionRepository { get; private set; }

        protected IRepository<Group, int> GroupRepository { get; private set; }

        protected IPrivilegeService PrivilegeService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.PermissionRepository = Substitute.For<IPermissionRepository>();
            this.GroupRepository = Substitute.For<IRepository<Group, int>>();
            this.PrivilegeService = new PrivilegeService(this.GroupRepository, this.PermissionRepository);

            this.Sut = new AuthorisationService(this.PrivilegeService);
        }
    }
}
