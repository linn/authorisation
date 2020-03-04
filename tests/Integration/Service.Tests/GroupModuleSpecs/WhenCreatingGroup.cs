namespace Linn.Authorisation.Service.Tests.GroupModuleSpecs
{
    using System.Collections.Generic;

    using Common.Facade;
    using Domain.Groups;
    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;

    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;
    using Resources;

    public class WhenCreatingGroup : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var resource = new GroupResource { Name = "Test", Active = true };
            var group = new Group("Test", true);
            this.GroupService.Add(Arg.Any<GroupResource>())
                .Returns(new CreatedResult<Group>(group));

            this.AuthorisationService.HasPermissionFor(
                AuthorisedAction.AuthorisationAdmin,
                Arg.Any<IEnumerable<string>>()).Returns(true);

            this.Response = this.Browser.Post(
                "/authorisation/groups",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Header("Content-Type", "application/json");
                    with.JsonBody(resource);
                }).Result;
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldCallService()
        {
            this.GroupService.Add(Arg.Any<GroupResource>()).ReceivedCalls();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<GroupResource>();
            resource.Active.Should().Be(true);
        }
    }
}