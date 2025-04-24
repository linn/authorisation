namespace Linn.Authorisation.Domain.Tests.GroupServiceTests
{
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Services;
    using Linn.Common.Persistence;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IRepository<Group, int> GroupRepository { get; private set; }

        protected IGroupService Sut { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.GroupRepository = Substitute.For<IRepository<Group, int>>();
            this.Sut = new GroupService(this.GroupRepository);
        }
    }
}