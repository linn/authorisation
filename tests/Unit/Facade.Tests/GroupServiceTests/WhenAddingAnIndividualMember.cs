namespace Linn.Authorisation.Facade.Tests.GroupServiceTests
{
    using System.Linq;
    using Common.Facade;
    using Domain.Groups;
    using FluentAssertions;
    using NSubstitute;
    using NUnit.Framework;
    using Resources;

    public class WhenAddingAnIndividualMember : ContextBase
    {
        private IResult<Group> result;

        [SetUp]
        public void SetUp()
        {
            var group = new Group("Test", true);

            var resource = new GroupMemberResource()
            {
                MemberUri = "/employees/7004",
                AddedByUri = "/employees/7004"
            };

            this.GroupRepository.FindById(1).Returns(group);

            this.result = this.Sut.AddGroupMember(1, resource);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<Group>>();

            var group = ((SuccessResult<Group>)this.result).Data;
            group.Name.Should().Be("Test");
            group.Active.Should().BeTrue();
        }

        [Test]
        public void ShouldHaveAddedIndividualMember()
        {
            var group = ((SuccessResult<Group>)this.result).Data;
            group.Members.Count.Should().Be(1);
            var individualMember = group.Members.First();
            individualMember.Should().BeOfType<IndividualMember>();
            ((IndividualMember) individualMember).MemberUri.Should().Be("/employees/7004");
        }
    }
}