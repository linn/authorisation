namespace Linn.Authorisation.Facade.Tests.PermissionTests
{
    using Domain.Repositories;
    using NSubstitute;
    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected AuthorisationService Sut { get; private set; }

        protected IPermissionRepository PermissionRepository { get; private set; }

        protected IGroupService GroupService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.PermissionRepository = Substitute.For<IPermissionRepository>();
            this.GroupService = Substitute.For<IGroupService>();

            this.Sut = new AuthorisationService(this.PermissionRepository, this.GroupService);
        }
    }
}
