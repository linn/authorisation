namespace Linn.Authorisation.Service.Tests.PrivilegesModuleSpecs
{
    using FluentAssertions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingPrivilege : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var resource = new PrivilegeResource { Name = "Test" };
            var privilege = new Privilege("Test", true);
            this.PrivilegeService.Add(Arg.Any<PrivilegeResource>())
                .Returns(new CreatedResult<Privilege>(privilege));
            this.Response = this.Browser.Post(
                "/authorisation/privileges",
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
            this.PrivilegeService.Add(Arg.Any<PrivilegeResource>()).ReceivedCalls();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PrivilegeResource>();
            resource.Name.Should().Be("Test");
            resource.Active.Should().Be(true);
        }
    }
}
