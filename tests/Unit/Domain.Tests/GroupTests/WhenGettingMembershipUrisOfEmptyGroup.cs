namespace Linn.Authorisation.Domain.Tests.GroupTests
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using NUnit.Framework;

    public class WhenGettingMembershipUrisOfEmptyGroup : ContextBase
    {
        private IEnumerable<string> Result;

        [SetUp]
        public void SetUp()
        {
            this.Result = this.Sut.MemberUris();
        }

        [Test]
        public void ShouldHaveNoUris()
        {
            this.Result.Count().Should().Be(0);
        }
    }
}