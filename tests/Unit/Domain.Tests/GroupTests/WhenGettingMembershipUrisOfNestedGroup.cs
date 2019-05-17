namespace Linn.Authorisation.Domain.Tests.GroupTests
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Groups;
    using NUnit.Framework;

    public class WhenGettingMembershipUrisOfNestedGroup : ContextBase
    {
        private IEnumerable<string> Result;

        [SetUp]
        public void SetUp()
        {
            this.Sut.AddIndividualMember("/employees/1", "/employees/7004");
            this.Sut.AddIndividualMember("/employees/2", "/employees/7004");

            var nestedGroup = new Group("Nested", true);
            nestedGroup.AddIndividualMember("/employees/3", "/employees/7004");
            nestedGroup.AddIndividualMember("/employees/4", "/employees/7004");

            this.Sut.AddGroupMember(nestedGroup, "/employees/7004");

            this.Result = this.Sut.MemberUris();
        }

        [Test]
        public void ShouldHaveNoUris()
        {
            this.Result.Count().Should().Be(4);
        }
    }
}