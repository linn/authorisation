namespace Linn.Authorisation.Service.Tests.GroupModuleSpecs
{
    using Common.Facade;
    using Domain.Groups;
    using FluentAssertions;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;
    using Resources;

    public class WhenRemovingGroupMember : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var group = new Group("Test", true);
            group.AddIndividualMember("/employees/1", "/employees/7004");
            group.Members[0].Id = 1;

            this.GroupService.RemoveGroupMember(1, 1).Returns(new SuccessResult<Group>(group));
            this.Response = this.Browser.Delete(
                "/authorisation/groups/1/members/1",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Header("Content-Type", "application/json");
                }).Result;
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.GroupService.AddGroupMember(1, Arg.Any<GroupMemberResource>()).ReceivedCalls();
        }
    }
}