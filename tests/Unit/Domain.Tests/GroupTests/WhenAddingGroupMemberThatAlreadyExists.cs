namespace Linn.Authorisation.Domain.Tests.GroupTests
{
    using Exceptions;
    using FluentAssertions;
    using Groups;
    using NUnit.Framework;

    public class WhenAddingGroupMemberThatAlreadyExists : ContextBase
    {
        private Group group;

        [SetUp]
        public void SetUp()
        {
            this.group = new Group("Toast", true) {Id = 1};
            this.Sut.AddGroupMember(this.group, "/employees/7004");
        }

        [Test]
        public void ShouldNotAddMemberAndThrowExceptionWhenAddedAgain()
        {
            var ex = Assert.Throws<MemberAlreadyInGroupException>(
                delegate { this.Sut.AddGroupMember(this.group, "/employees/7004"); });
            this.Sut.Members.Count.Should().Be(1);

            ex.Message.Should().Be("group Toast already exists in group Test");
        }
    }
}