namespace Linn.Authorisation.Facade.Tests.GroupServiceTests
{
    using System.Linq;
    using Common.Facade;
    using Domain.Groups;
    using FluentAssertions;
    using NSubstitute;
    using NUnit.Framework;
    using Resources;

    public class WhenAddingAnIndividualMemberWhoIsAlreadyAMember : ContextBase
    {
        private IResult<Group> result;

        [SetUp]
        public void SetUp()
        {
            var group = new Group("Test", true);
            group.AddIndividualMember("/employees/7004", "/employees/7004");

            var resource = new GroupMemberResource()
            {
                MemberUri = "/employees/7004",
                AddedByUri = "/employees/7004"
            };

            this.GroupRepository.FindById(1).Returns(group);

            this.result = this.Sut.AddGroupMember(1, resource);
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.result.Should().BeOfType<BadRequestResult<Group>>();

            ((BadRequestResult<Group>) this.result).Message.Should().Be("/employees/7004 already exists in group Test");
        }
    }
}