namespace Linn.Authorisation.Domain.Tests.GroupTests
{
    using Exceptions;
    using FluentAssertions;
    using NUnit.Framework;

    public class WhenAddingIndividualMemberThatAlreadyExists : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.AddIndividualMember("/employees/1", "/employees/7004");
        }

        [Test]
        public void ShouldNotAddMemberAndThrowExceptionWhenAddedAgain()
        {
            var ex = Assert.Throws<MemberAlreadyInGroupException>(
                delegate { this.Sut.AddIndividualMember("/employees/1", "/employees/7004"); });
            this.Sut.Members.Count.Should().Be(1);

            ex.Message.Should().Be("/employees/1 already exists in group Test");
        }
    }
}