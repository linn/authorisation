namespace Linn.Authorisation.Facade.Tests.GroupServiceTests
{
    using Common.Facade;
    using Domain.Groups;
    using FluentAssertions;
    using NSubstitute;
    using NUnit.Framework;
    using Resources;

    public class WhenAddingAGroupMemberToNonExistentGroup : ContextBase
    {
        private IResult<Group> result;

        [SetUp]
        public void SetUp()
        {
            var resource = new GroupMemberResource()
            {
                MemberUri = "/employees/7004",
                AddedByUri = "/employees/7004"
            };

            this.GroupRepository.FindById(1).Returns((Group)null);

            this.result = this.Sut.AddGroupMember(1, resource);
        }

        [Test]
        public void ShouldReturnNotFound()
        {
            this.result.Should().BeOfType<NotFoundResult<Group>>();

            ((NotFoundResult<Group>)this.result).Message.Should().Be("group 1 not found");
        }
    }
}