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

    public class WhenRemovingAMemberTheIsntAMember : ContextBase
    {
        private IResult<Group> result;

        [SetUp]
        public void SetUp()
        {
            var group = new Group("Change UK", true);

            this.GroupRepository.FindById(1).Returns(group);

            this.result = this.Sut.RemoveGroupMember(1, 1);
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.result.Should().BeOfType<NotFoundResult<Group>>();

            ((NotFoundResult<Group>)this.result).Message.Should().Be("member 1 not found on group Change UK");
        }
    }
}