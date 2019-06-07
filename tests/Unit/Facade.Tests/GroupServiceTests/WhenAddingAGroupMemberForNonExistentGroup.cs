namespace Linn.Authorisation.Facade.Tests.GroupServiceTests
{
    using System;
    using System.Linq.Expressions;
    using Common.Facade;
    using Domain.Groups;
    using FluentAssertions;
    using NSubstitute;
    using NUnit.Framework;
    using Resources;

    public class WhenAddingAGroupMemberForNonExistentGroup : ContextBase
    {
        private IResult<Group> result;

        [SetUp]
        public void SetUp()
        {
            var group = new Group("Test", true);

            var resource = new GroupMemberResource()
            {
                GroupName = "Michael Gove Fan Club",
                AddedByUri = "/employees/7004"
            };

            this.GroupRepository.FindById(1).Returns(group);
            this.GroupRepository.FindBy(Arg.Any<Expression<Func<Group, bool>>>()).Returns((Group) null);

            this.result = this.Sut.AddGroupMember(1, resource);
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.result.Should().BeOfType<BadRequestResult<Group>>();

            ((BadRequestResult<Group>) this.result).Message.Should().Be("Group Michael Gove Fan Club does not exist");
        }
    }
}