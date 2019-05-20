namespace Linn.Authorisation.Domain.Tests.GroupTests
{
    using FluentAssertions;
    using NUnit.Framework;

    public class WhenCheckingMembershipOfEmptyGroup : ContextBase
    {
        private bool Result;

        [SetUp]
        public void SetUp()
        {
            this.Result = this.Sut.IsMemberOf("/employees/1");
        }

        [Test]
        public void ShouldNotBeAMember()
        {
            this.Result.Should().BeFalse();
        }
    }
}