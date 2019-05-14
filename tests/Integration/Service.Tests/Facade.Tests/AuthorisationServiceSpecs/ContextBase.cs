namespace Linn.Authorisation.Integration.Tests.Facade.Tests.AuthorisationServiceSpecs
{
    using Common.Persistence;
    using Domain;
    using NUnit.Framework;
    using Service.Tests.Facade.Tests;
    using Authorisation.Facade;

    public abstract class ContextBase
    {
        protected AuthorisationService Sut { get; private set; }

        protected IRepository<Role, int> RoleRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            TestDbContext.SetUp();
            this.RoleRepository = new TestRoleRepository();
            this.Sut = new AuthorisationService(this.RoleRepository);
        }
    }
}
