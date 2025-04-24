namespace Linn.Authorisation.Domain.Tests.PrivilegeServiceTests
{
    using Linn.Authorisation.Domain.Services;
    using Linn.Common.Persistence;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IRepository<Privilege, int> PrivilegeRepository { get; private set; }

        protected IPrivilegeService Sut { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.PrivilegeRepository = Substitute.For<IRepository<Privilege, int>>();
            this.Sut = new PrivilegeService(this.PrivilegeRepository);
        }
    }
}

