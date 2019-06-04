namespace Linn.Authorisation.Facade.Tests.GroupServiceTests
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
    using Common.Facade;
    using Domain.Groups;
    using FluentAssertions;
    using NSubstitute;
    using NUnit.Framework;
    using Resources;

    public class WhenAddingAGroupMember : ContextBase
    {
        private IResult<Group> result;

        [SetUp]
        public void SetUp()
        {
            var group = new Group("Test", true);
            var subGroup = new Group("Sub", true);

            var resource = new GroupMemberResource()
            {
                GroupName = "Sub",
                AddedByUri = "/employees/7004"
            };

            this.GroupRepository.FindById(1).Returns(group);
            this.GroupRepository.FindBy(Arg.Any<Expression<Func<Group, bool>>>()).Returns(subGroup);

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
        public void ShouldHaveAddedGroupMember()
        {
            var group = ((SuccessResult<Group>)this.result).Data;
            group.Members.Count.Should().Be(1);
            var groupMember = group.Members.First();
            groupMember.Should().BeOfType<GroupMember>();
            ((GroupMember) groupMember).Group.Name.Should().Be("Sub");
        }
    }
}