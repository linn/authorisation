namespace Linn.Authorisation.Service.Tests.Facade.Tests.AuthorisationServiceSpecs
{
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Facade;
    using Linn.Authorisation.Service.Tests.Facade.Tests;
    using Linn.Common.Persistence;

    using NUnit.Framework;

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
