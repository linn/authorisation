namespace Linn.Authorisation.Facade.Tests.GroupServiceTests
{
    using Common.Persistence;
    using Domain;
    using Domain.Groups;
    using NSubstitute;
    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected GroupService Sut { get; private set; }

        protected IRepository<Group, int> GroupRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.GroupRepository = Substitute.For<IRepository<Group, int>>();
            this.Sut = new GroupService(
                this.GroupRepository,
                this.TransactionManager
               );
        }
    }
}
