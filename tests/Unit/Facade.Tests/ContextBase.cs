namespace Linn.Authorisation.Facade.Tests
{
    using Linn.Authorisation.Domain;
    using Linn.Common.Persistence;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected AuthorisationService Sut { get; private set; }

        protected IRepository<Role, int> RoleRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.RoleRepository = Substitute.For<IRepository<Role, int>>();
            this.Sut = new AuthorisationService(this.RoleRepository);
        }
    }
}
