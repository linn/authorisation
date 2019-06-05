namespace Linn.Authorisation.Facade.Tests.GroupServiceTests
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Common.Facade;
    using Domain.Groups;
    using FluentAssertions;
    using NSubstitute;
    using NUnit.Framework;
    using Resources;

    public class WhenAddingAGroupMemberWithoutMemberUriOrGroup : ContextBase
    {
        private IResult<Group> result;

        [SetUp]
        public void SetUp()
        {
            var group = new Group("Test", true);

            var resource = new GroupMemberResource()
            {
                AddedByUri = "/employees/7004"
            };

            this.GroupRepository.FindById(1).Returns(group);

            this.result = this.Sut.AddGroupMember(1, resource);
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.result.Should().BeOfType<BadRequestResult<Group>>();

            ((BadRequestResult<Group>)this.result).Message.Should().Be("No member or group supplied");
        }
    }
}