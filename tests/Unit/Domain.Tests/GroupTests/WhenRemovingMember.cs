namespace Linn.Authorisation.Domain.Tests.GroupTests
{
    using System.Linq;
    using FluentAssertions;
    using Groups;
    using NUnit.Framework;

    public class WhenRemovingMember : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.AddIndividualMember("/employees/1", "/employees/7004");
            var member = this.Sut.Members.First();
            this.Sut.RemoveMember(member);
        }

        [Test]
        public void ShouldHaveDisappearedAgain()
        {
            this.Sut.Members.Count.Should().Be(0);
        }
    }
}