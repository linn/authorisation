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

    public class WhenRemovingAGroupMember : ContextBase
    {
        private IResult<Group> result;

        [SetUp]
        public void SetUp()
        {
            var group = new Group("Test", true) { Id = 1 };
            var subGroup = new Group("Sub", true) { Id = 2 };
            
            group.AddGroupMember(subGroup, "/employees/7004");
            group.Members[0].Id = 1;

            this.GroupRepository.FindById(1).Returns(group);

            this.result = this.Sut.RemoveGroupMember(1, 1);
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
        public void ShouldHaveRemovedGroupMember()
        {
            var group = ((SuccessResult<Group>)this.result).Data;
            group.Members.Count.Should().Be(0);
        }
    }
}