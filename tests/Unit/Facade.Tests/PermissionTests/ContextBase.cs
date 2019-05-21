namespace Linn.Authorisation.Facade.Tests.PermissionTests
{
    using Domain.Repositories;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Common.Persistence;

    using NSubstitute;
    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected AuthorisationService Sut { get; private set; }

        protected IPermissionRepository PermissionRepository { get; private set; }

        protected IRepository<Group, int> GroupRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.PermissionRepository = Substitute.For<IPermissionRepository>();
            this.GroupRepository = Substitute.For<IRepository<Group, int>>();

            this.Sut = new AuthorisationService(this.PermissionRepository, this.GroupRepository);
        }
    }
}
