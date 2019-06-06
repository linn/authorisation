namespace Linn.Authorisation.Domain.Tests.GroupTests
{
    using System.Linq;
    using FluentAssertions;
    using Groups;
    using NUnit.Framework;

    public class WhenAddingMember : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.AddIndividualMember("/employees/1", "/employees/7004");
        }

        [Test]
        public void ShouldAddIndividualMember()
        {
            this.Sut.Members.Count.Should().Be(1);
            var member = this.Sut.Members.First();
            member.Should().BeOfType<IndividualMember>();
        }
    }
}