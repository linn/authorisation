namespace Linn.Authorisation.Domain.Tests.GroupTests
{
    using Exceptions;
    using FluentAssertions;
    using Groups;
    using NUnit.Framework;

    public class WhenAddingGroupMemberToItself : ContextBase
    {
        private Group group;

        [SetUp]
        public void SetUp()
        {
            this.Sut.Id = 1;
            this.group = new Group("Test", true) { Id = 1 };
        }

        [Test]
        public void ShouldNotAddMemberAndThrowExceptionWhenAddedAgain()
        {
            var ex = Assert.Throws<MemberAlreadyInGroupException>(
                delegate { this.Sut.AddGroupMember(this.group, "/employees/7004"); });
            this.Sut.Members.Count.Should().Be(0);

            ex.Message.Should().Be("cannot make Test a member of itself");
        }
    }
}