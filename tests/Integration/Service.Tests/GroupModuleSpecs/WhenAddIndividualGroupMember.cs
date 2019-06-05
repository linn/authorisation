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

    public class WhenAddIndividualGroupMember : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var resource = new GroupMemberResource
            {
                MemberUri = "/employees/1",
                AddedByUri = "/employees/7004"
            };
            var group = new Group("Test", true);
            group.AddIndividualMember("/employees/1", "/employees/7004");
            this.GroupService.AddGroupMember(1, Arg.Any<GroupMemberResource>()).Returns(new SuccessResult<Group>(group));
            this.Response = this.Browser.Post(
                "/authorisation/groups/1/members",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Header("Content-Type", "application/json");
                    with.JsonBody(resource);
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

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<GroupResource>();
            resource.Active.Should().Be(true);
        }
    }
}